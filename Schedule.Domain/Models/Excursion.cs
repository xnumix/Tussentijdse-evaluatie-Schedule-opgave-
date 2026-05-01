namespace Schedule.Domain.Models;

public class Excursion : Lesson
{
    private int _travelTimeInMinutes;
    private TimeOnly _endTime;
    private int _studentCount;

    public Excursion(TimeOnly startTime, string name, int studentCount, int travelTime) : base(startTime, name, studentCount)
    {
        TravelTimeInMinutes = travelTime;
    }

    //properties
    public int TravelTimeInMinutes
    {
        get => _travelTimeInMinutes;
        init
        {
            if (value < 1 || value > 120)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Traveltime must be between 1 and 120.");
            }
            _travelTimeInMinutes = value;
        }
    }


    public TimeOnly EndTime
    {
        get => _endTime;
        init
        {
            int durationInMinutes = 10 * StudentCount;
            int travelDuration = TravelTimeInMinutes * 2;

            int totalDuration = durationInMinutes + travelDuration;

            _endTime = Domain.Models.TimeHelper.CalculateEndTime(StartTime, totalDuration);
        }
    }

    public int StudentCount
    {
        get => _studentCount;
        init
        {
            if (value < 1 || value >20)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Studentcount for excursion must be between 1 and 20.");
            }

            _studentCount = value;
        }
    }

    public override string? ToString()
    {
        return $"{GetType().Name} to {Name} with {StudentCount} students";
    }
}
