using Schedule.Domain.Models;

namespace Schedule.Domain.Repository;

public interface IDayRepository
{
    void StoreDay(Day day);
    IReadOnlyList<Day> GetDays();
    Day? GetDay(DateOnly date);
}
