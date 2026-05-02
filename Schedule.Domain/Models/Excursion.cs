using System.Xml.Linq;

namespace Schedule.Domain.Models;

public class Excursion : Lesson
{
    public Excursion(int travelTime, TimeOnly startTime, string name, int studentCount) : base(startTime, name, studentCount)
    {
        TravelTimeInMinutes = travelTime;
    }
   
    public int TravelTimeInMinutes
    {
        get;
        init
        {
            if (value < 1 || value > 120)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Traveltime must be between 1 and 120.");
            }
            field = value;
        }
    }

    public TimeOnly EndTime
    {
        get;
        init
        {
            int durationInMinutes = 10 * StudentCount;
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
