
namespace Schedule.Domain.Models;

public class Lesson : Activity
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

    // om ervoor te zorgen dat de endtime overriden wordt vanuit de abstracte klasse, anders heb ik 2 endtimes
    public override TimeOnly EndTime
    {
        get
        {
            int durationInMinutes = ammountOfMinutesPerStudent * StudentCount;

            return TimeHelper.CalculateEndTime(StartTime, durationInMinutes);
        }
    }
    
    public override string GetCategory() => "Lesson";

    public override string? ToString()
    {
        return $"{base.ToString()} - {EndTime} - {GetCategory()} {Name} with {StudentCount} students";
    }

}
