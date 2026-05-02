using Schedule.Domain.Repository;

namespace Schedule.Domain.Models;

public class Lesson : IPlannableActivity
{
  
    public Lesson(TimeOnly startTime, string name, int studentCount)
    {
        StartTime = startTime;
        Name = name;
        StudentCount = studentCount;
    }

    public string Name
    {
        get;
        set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace("Fill in a valid name.");
            field = value;
        }
    }

    public int StudentCount
    {
        get;
        init
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Studentcount for lesson must be atleast 1.");
            }
            field = value;
        }
    }

    public TimeOnly StartTime
    {
        get;
    }

    public TimeOnly EndTime
    {
        get;
        init
        {
            int durationInMinutes = 10 * StudentCount;

            value = TimeHelper.CalculateEndTime(StartTime, durationInMinutes);
            field = value;
        }
    }


    public int CompareTo(IPlannableActivity? other)
    {
        //if (other == null)
            return 1;
    }

    int IComparable<IPlannableActivity>.CompareTo(IPlannableActivity? other)
    {
        throw new NotImplementedException();
    }

    public override string? ToString()
    {
        return $"{GetType().Name}{Name} with {StudentCount} students";
    }

    
}
