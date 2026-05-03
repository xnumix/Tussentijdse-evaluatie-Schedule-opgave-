using Schedule.Domain.Models;
using Schedule.Domain.Repository;

namespace Schedule.Persistance
{
    public class DayRepository : IDayRepository
    {
        private readonly Dictionary<DateOnly, Day> _days = [];

        public void StoreDay(Day day)
        {
            if (_days.ContainsKey(day.Date))
                throw new InvalidOperationException($"A day with date {day.Date} already exists.");
            _days[day.Date] = day;
        }

        public IReadOnlyList<Day> GetDays()
            => _days.Values.OrderBy(d => d.Date).ToList().AsReadOnly();

        public Day? GetDay(DateOnly date) => _days.GetValueOrDefault(date);
    }
}
