using Schedule.Domain.Models;
using Schedule.Domain.Repository;

namespace Schedule.Domain
{
    public class DomainManager
    {
        //Dependency injection van de persistance laag
        private readonly IActivityRepository _repository;

        public DomainManager(IActivityRepository repository)
        {
            _repository = repository;
        }

        public void CreateNewLesson(TimeOnly StartDay, TimeOnly EndDay, TimeOnly starttime, string name, int studentCount)
            => StoreActivity(new Lesson(starttime, name, studentCount), StartDay, EndDay);

        public void CreateNewExcursion(TimeOnly StartDay, TimeOnly EndDay, int travelTime, TimeOnly starttime, string name, int studentCount)
            => StoreActivity(new Excursion(travelTime, starttime, name, studentCount), StartDay, EndDay);

        public void CreateNewBreak(TimeOnly StartDay, TimeOnly EndDay,TimeOnly starttime, int lengthBreak)
            => StoreActivity(new Break(starttime, lengthBreak), StartDay, EndDay);

        public IReadOnlyList<IPlannableActivity>ListActivities() => _repository.GetActivities();

        public void StoreActivity(Activity activity, TimeOnly StartDay, TimeOnly EndDay)
        {
            bool isInScheduele= TimeHelper.FitsInSchedule(activity, StartDay, EndDay);

            if (isInScheduele == false)
            {
                throw new OutOfScheduleRangeException("Does not fit in schedule", StartDay, EndDay);
            }
            else _repository.StoreActivity(activity);
        }
}
}
