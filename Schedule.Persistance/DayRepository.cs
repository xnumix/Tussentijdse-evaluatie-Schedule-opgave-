using Microsoft.Data.SqlClient;
using Schedule.Domain.Exceptions;
using Schedule.Domain.Models;
using Schedule.Domain.Repository;

namespace Schedule.Persistance
{
    public class DayRepository : IDayRepository
    {
        private readonly string _connectionString;

        public DayRepository(string connectionString)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
            _connectionString = connectionString;
        }

        public void StoreDay(Day day)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            try
            {
                using SqlCommand command = new(
                    "INSERT INTO Days (Date, StartTime, EndTime) VALUES (@Date, @StartTime, @EndTime);",
                    connection);
                command.Parameters.AddWithValue("@Date", day.Date.ToDateTime(TimeOnly.MinValue));
                command.Parameters.AddWithValue("@StartTime", day.StartTime.ToTimeSpan());
                command.Parameters.AddWithValue("@EndTime", day.EndTime.ToTimeSpan());
                command.ExecuteNonQuery();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new DuplicateDateException($"A day with date {day.Date} already exists.", day.Date);
            }
        }

        public IReadOnlyList<Day> GetDays()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            List<(int Id, Day Day)> rows = [];
            using (SqlCommand command = new(
                "SELECT Id, Date, StartTime, EndTime FROM Days ORDER BY Date;",
                connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    DateOnly date = DateOnly.FromDateTime(reader.GetDateTime(1));
                    TimeOnly start = TimeOnly.FromTimeSpan(reader.GetTimeSpan(2));
                    TimeOnly end = TimeOnly.FromTimeSpan(reader.GetTimeSpan(3));
                    rows.Add((id, new Day(date, start, end)));
                }
            }

            foreach ((int id, Day day) in rows)
                LoadActivities(connection, id, day);

            return rows.Select(r => r.Day).ToList().AsReadOnly();
        }

        public Day? GetDay(DateOnly date)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            int dayId;
            Day day;
            using (SqlCommand command = new(
                "SELECT Id, Date, StartTime, EndTime FROM Days WHERE Date = @Date;",
                connection))
            {
                command.Parameters.AddWithValue("@Date", date.ToDateTime(TimeOnly.MinValue));
                using SqlDataReader reader = command.ExecuteReader();
                if (!reader.Read()) return null;
                dayId = reader.GetInt32(0);
                day = new Day(
                    DateOnly.FromDateTime(reader.GetDateTime(1)),
                    TimeOnly.FromTimeSpan(reader.GetTimeSpan(2)),
                    TimeOnly.FromTimeSpan(reader.GetTimeSpan(3)));
            }

            LoadActivities(connection, dayId, day);
            return day;
        }

        public void AddActivity(Day day, Activity activity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            int dayId = GetDayId(connection, day.Date)
                ?? throw new DayNotFoundException($"No day exists with date {day.Date}.", day.Date);

            using SqlCommand command = new(
                @"INSERT INTO Activities (DayId, Type, StartTime, Name, StudentCount, DurationInMinutes, TravelTimeInMinutes)
                  VALUES (@DayId, @Type, @StartTime, @Name, @StudentCount, @DurationInMinutes, @TravelTimeInMinutes);",
                connection);

            command.Parameters.AddWithValue("@DayId", dayId);
            command.Parameters.AddWithValue("@StartTime", activity.StartTime.ToTimeSpan());

            switch (activity)
            {
                case Excursion excursion:
                    command.Parameters.AddWithValue("@Type", "Excursion");
                    command.Parameters.AddWithValue("@Name", excursion.Name);
                    command.Parameters.AddWithValue("@StudentCount", excursion.StudentCount);
                    command.Parameters.AddWithValue("@DurationInMinutes", DBNull.Value);
                    command.Parameters.AddWithValue("@TravelTimeInMinutes", excursion.TravelTimeInMinutes);
                    break;
                case Lesson lesson:
                    command.Parameters.AddWithValue("@Type", "Lesson");
                    command.Parameters.AddWithValue("@Name", lesson.Name);
                    command.Parameters.AddWithValue("@StudentCount", lesson.StudentCount);
                    command.Parameters.AddWithValue("@DurationInMinutes", DBNull.Value);
                    command.Parameters.AddWithValue("@TravelTimeInMinutes", DBNull.Value);
                    break;
                case Break br:
                    command.Parameters.AddWithValue("@Type", "Break");
                    command.Parameters.AddWithValue("@Name", DBNull.Value);
                    command.Parameters.AddWithValue("@StudentCount", DBNull.Value);
                    command.Parameters.AddWithValue("@DurationInMinutes", br.DurationInMinutes);
                    command.Parameters.AddWithValue("@TravelTimeInMinutes", DBNull.Value);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown activity type: {activity.GetType().Name}");
            }

            command.ExecuteNonQuery();
        }

        private static int? GetDayId(SqlConnection connection, DateOnly date)
        {
            using SqlCommand command = new("SELECT Id FROM Days WHERE Date = @Date;", connection);
            command.Parameters.AddWithValue("@Date", date.ToDateTime(TimeOnly.MinValue));
            object? result = command.ExecuteScalar();
            return result is null or DBNull ? null : (int)result;
        }

        private static void LoadActivities(SqlConnection connection, int dayId, Day day)
        {
            using SqlCommand command = new(
                @"SELECT Type, StartTime, Name, StudentCount, DurationInMinutes, TravelTimeInMinutes
                  FROM Activities WHERE DayId = @DayId ORDER BY StartTime;",
                connection);
            command.Parameters.AddWithValue("@DayId", dayId);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string type = reader.GetString(0);
                TimeOnly start = TimeOnly.FromTimeSpan(reader.GetTimeSpan(1));
                Activity activity = type switch
                {
                    "Lesson" => new Lesson(start, reader.GetString(2), reader.GetInt32(3)),
                    "Excursion" => new Excursion(reader.GetInt32(5), start, reader.GetString(2), reader.GetInt32(3)),
                    "Break" => new Break(start, reader.GetInt32(4)),
                    _ => throw new InvalidOperationException($"Unknown activity type in DB: {type}")
                };
                day.AddActivity(activity);
            }
        }
    }
}
