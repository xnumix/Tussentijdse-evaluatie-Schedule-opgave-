using Schedule.Domain.DTO;
using Schedule.Domain.Models;
using Schedule.Domain.Repository;

namespace Schedule.Domain
{
    public class DomainManager
    {
        //Dependency injection van de persistance laag
        private readonly IDayRepository _repository;

        public DomainManager(IDayRepository repository)
        {
            _repository = repository;
        }

        public void CreateNewDay(DateOnly date, TimeOnly startTime, TimeOnly endTime)
            => _repository.StoreDay(new Day(date, startTime, endTime));

        public void CreateNewLesson(DateOnly dayDate, TimeOnly starttime, string name, int studentCount)
        {
            Day day = GetDayOrThrow(dayDate);
            Lesson lesson = new(starttime, name, studentCount);
            day.AddActivity(lesson);
            _repository.AddActivity(day, lesson);
        }

        public void CreateNewExcursion(DateOnly dayDate, int travelTime, TimeOnly starttime, string name, int studentCount)
        {
            Day day = GetDayOrThrow(dayDate);
            Excursion excursion = new(travelTime, starttime, name, studentCount);
            day.AddActivity(excursion);
            _repository.AddActivity(day, excursion);
        }

        public void CreateNewBreak(DateOnly dayDate, TimeOnly starttime, int lengthBreak)
        {
            Day day = GetDayOrThrow(dayDate);
            Break br = new(starttime, lengthBreak);
            day.AddActivity(br);
            _repository.AddActivity(day, br);
        }

        public IReadOnlyList<DayDto> ListDays()
            => _repository.GetDays().Select(ToDto).ToList().AsReadOnly();

        public DayDto? GetDay(DateOnly date)
        {
            Day? day = _repository.GetDay(date);
            return day is null ? null : ToDto(day);
        }

        private static DayDto ToDto(Day day)
        {
            IReadOnlyList<ActivityDto> activities = day.Activities
                .Select(a => new ActivityDto(a.StartTime, a.EndTime, a.ToString() ?? string.Empty))
                .ToList()
                .AsReadOnly();
            return new DayDto(day.Date, day.StartTime, day.EndTime, activities);
        }

        private Day GetDayOrThrow(DateOnly date)
        {
            Day? day = _repository.GetDay(date);
            if (day is null)
                throw new InvalidOperationException($"No day exists with date {date}.");
            return day;
        }
    }
}
