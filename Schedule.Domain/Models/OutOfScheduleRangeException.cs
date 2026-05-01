namespace Schedule.Domain.Models;

internal class OutOfScheduleRangeException : Exception
{
        public OutOfScheduleRangeException()
    {
    }

    public OutOfScheduleRangeException(string message)
        : base(message)
    {
    }

    public OutOfScheduleRangeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
