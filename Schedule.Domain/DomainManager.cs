using Schedule.Domain.Models;
using Schedule.Domain.Repository;
using System.Xml.Linq;

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

        public void CreateNewLesson(string name, TimeOnly starttime, int studentCount)
        {
            //_repository.StoreActivity();
        }

        public Lesson CreateNewLesson(TimeOnly starttime, string name, int studentCount) 
        {
            List<Lesson> Lessons = [];

            Lesson lesson = new(starttime,name,studentCount);
            Lessons.Add(lesson);

            foreach (var Lesson in Lessons)
                Console.WriteLine(Lesson);

            return lesson;
        }

        public Excursion CreateNewExcursion(int travelTime, TimeOnly starttime, string name, int studentCount)
        {
            List<Excursion> Excursions = [];

            Excursion excursion = new(travelTime, starttime, name, studentCount);
            Excursions.Add(excursion);

            foreach (var Excursion in Excursions)
                Console.WriteLine(Excursion);

            return excursion;
        }

        public Break CreateNewBreak(TimeOnly starttime, int lengthBreak)
        {
            List<Break> Pauzes = [];

            Break pauze = new(starttime, lengthBreak);
            Pauzes.Add(pauze);

            foreach (var Pauze in Pauzes)
                Console.WriteLine(Pauze);

            return pauze;
        }

        
    }
}
