using Schedule.Domain.Repository;

namespace Schedule.Domain.Models;

public class Lesson : IPlannableActivity
{

    //fields 
    private TimeOnly _startTime;
    private TimeOnly _endTime;
    private string _name;
    private int _studentCount;
    private int _durationInMinutes;

    public Lesson(TimeOnly startTime, string name, int studentCount)
    {
        _startTime = startTime;
        Name = name;
        StudentCount = studentCount;
    }

    // properties

    public string Name
    {
        get => _name;
        set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace("Fill in a valid name.");
            _name = value;
        }
    }

    public int StudentCount
    {
        get => _studentCount;
        init
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Studentcount for lesson must be at least 1.");
            }
            _studentCount = value;
        }
    }

    public TimeOnly StartTime
    {
        get => _startTime;

    }

    public TimeOnly EndTime
    {

        get => _endTime;
        init
        {
            int durationInMinutes = 10 * StudentCount;

            value = Domain.Models.TimeHelper.CalculateEndTime(StartTime, durationInMinutes);
            _endTime = value;
        }
    }

    TimeOnly IPlannableActivity.StartTime { get => StartTime; init => throw new NotImplementedException(); }

    public int CompareTo(IPlannableActivity? other)
    {
        throw new NotImplementedException();
    }

    public override string? ToString()
    {
        return $"{GetType().Name}{Name} with {StudentCount} students";
    }
}
