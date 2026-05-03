namespace Schedule.Domain.Models;

public class Excursion : Lesson
{
    const int minimumTravelTime = 1;
    const int maximumTravelTime = 120;
    const int ammountOfMinutesPerStudent = 10;
    const int tripToAndFromSchool = 2;

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
        get
        {
            int durationInMinutes = ammountOfMinutesPerStudent * StudentCount;
            int travelDuration = TravelTimeInMinutes * tripToAndFromSchool;

            int totalDuration = durationInMinutes + travelDuration;

            return TimeHelper.CalculateEndTime(StartTime, totalDuration);
        }
    }

    public override string GetCategory() => "Excursion";

    public override string? ToString()
    {
        return $"{base.ToString()}";
    }
}
