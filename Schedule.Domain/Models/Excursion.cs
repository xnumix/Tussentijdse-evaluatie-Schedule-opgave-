namespace Schedule.Domain.Models;

public class Excursion : Lesson
{
    const int minimumTravelTime = 1;
    const int maximumTravelTime = 120;
    const int ammountOfMinutesPerStudent = 10;

    public Excursion(int travelTime, TimeOnly startTime, string name, int studentCount) : base(startTime, name, studentCount)
    {
        TravelTimeInMinutes = travelTime;
    }

    public int TravelTimeInMinutes
    {
        get;
        init
        {
            if (value < minimumTravelTime || value > maximumTravelTime)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Traveltime must be between {minimumTravelTime} and {maximumTravelTime}.");
            }
            field = value;
        }
    }

    public TimeOnly EndTime
    {
        get;
        init
        {
            int durationInMinutes = ammountOfMinutesPerStudent * StudentCount;
            int travelDuration = TravelTimeInMinutes * 2;

            int totalDuration = durationInMinutes + travelDuration;

            field = TimeHelper.CalculateEndTime(StartTime, totalDuration);
        }
    }

    public override string? ToString()
    {
        return $"{GetType().Name} to {Name} with {StudentCount} students";
    }
}
