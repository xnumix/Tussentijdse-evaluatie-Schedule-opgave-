namespace Schedule.Domain.Repository;

public interface IPlannableActivity : IComparable<IPlannableActivity>
{
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }
}

//Readonly