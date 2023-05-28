namespace ExpenseTrackerBackend.Infrastructure;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}