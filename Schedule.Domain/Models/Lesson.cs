using Schedule.Domain.Repository;

namespace Schedule.Domain.Models;

public class Lesson : Activity, IPlannableActivity
{
    const int minimumStudentCount = 1;
    const int ammountOfMinutesPerStudent = 10;

    public Lesson(TimeOnly startTime, string name, int studentCount) : base(startTime)
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


    int IComparable<IPlannableActivity>.CompareTo(IPlannableActivity? other)
    {
        throw new NotImplementedException();
    }
    
    public override string GetCategory() => "Lesson";

    public override string? ToString()
    {
        return $"{base.ToString()} - {EndTime} - {GetCategory()} {Name} with {StudentCount} students";
    }

}
