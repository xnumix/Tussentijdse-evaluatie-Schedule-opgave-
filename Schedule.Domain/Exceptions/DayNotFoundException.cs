namespace Schedule.Domain.Exceptions;

public class DayNotFoundException : Exception
{
    public DateOnly Date { get; }

    public DayNotFoundException()
    {
    }

    public DayNotFoundException(string message, DateOnly date)
        : base(message)
    {
        Date = date;
    }
}
