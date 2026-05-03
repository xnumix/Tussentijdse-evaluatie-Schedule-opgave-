namespace Schedule.Domain.Exceptions;

public class OutOfScheduleRangeException : Exception
{
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }

    public OutOfScheduleRangeException()
    {
    }

    public OutOfScheduleRangeException(string message, TimeOnly DayStart, TimeOnly DayEnd)
        : base(message)
    {
        StartTime = DayStart;
        EndTime = DayEnd;
    }
}
