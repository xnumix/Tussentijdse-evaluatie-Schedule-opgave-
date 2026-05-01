namespace Schedule.Domain.Repository;

public interface IPlannableActivity : IComparable<IPlannableActivity>
{
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }
}

//Readonly