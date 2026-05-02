using Schedule.Domain.Repository;

namespace Schedule.Domain.Models;

public class Lesson : IPlannableActivity
{
    const int minimumStudentCount = 1;
    const int ammountOfMinutesPerStudent = 10;

    public Lesson(TimeOnly startTime, string name, int studentCount)
    {
        Name = name;
        StudentCount = studentCount;
    }

    public string Name
    {
        get;
        set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Name));
            field = value;
        }
    }

    public int StudentCount
    {
        get;
        init
        {
            if (value < minimumStudentCount)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Studentcount for lesson must be atleast {minimumStudentCount}.");
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

            value = TimeHelper.CalculateEndTime(StartTime, durationInMinutes);
            field = value;
        }
    }

    public TimeOnly StartTime => throw new NotImplementedException();

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
        return $"{StartTime} - {EndTime} - {GetType().Name}{Name} with {StudentCount} students";
    }

    
}
