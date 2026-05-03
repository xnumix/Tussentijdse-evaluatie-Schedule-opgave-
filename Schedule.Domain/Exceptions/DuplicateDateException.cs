namespace Schedule.Domain.Exceptions;

public class DuplicateDateException : Exception
{
    public DateOnly Date { get; }

    public DuplicateDateException()
    {
    }

    public DuplicateDateException(string message, DateOnly date)
        : base(message)
    {
        Date = date;
    }
}
