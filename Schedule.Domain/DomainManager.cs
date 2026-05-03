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

        public void CreateNewLesson(TimeOnly starttime, string name, int studentCount)
            => _repository.StoreActivity(new Lesson(starttime, name, studentCount));

        public void CreateNewExcursion(int travelTime, TimeOnly starttime, string name, int studentCount)
            => _repository.StoreActivity(new Excursion(travelTime, starttime, name, studentCount));

        public void CreateNewBreak(TimeOnly starttime, int lengthBreak)
            => _repository.StoreActivity(new Break(starttime, lengthBreak));

        public IReadOnlyList<Activity> ListActivities() => _repository.GetActivities();
    }
}
