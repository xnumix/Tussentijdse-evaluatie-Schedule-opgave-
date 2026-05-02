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



        public Lesson CreateNewLesson() 
        {
            List<Lesson> Lessons = [];

            Console.WriteLine("Give the name of the lesson:");
            string name = Console.ReadLine();

            Console.WriteLine("Give the start-time of lesson:");
            TimeOnly starttime= TimeOnly.Parse( Console.ReadLine());

            Console.WriteLine("Give the ammount of students:");
            int studentCount = int.Parse(Console.ReadLine());

            Lesson lesson = new(starttime,name,studentCount);
            Lessons.Add(lesson);

            foreach (var Lesson in Lessons)
                Console.WriteLine(Lesson);

            return lesson;
        }

        public Break CreateNewBreak()
        {
            List<Break> Pauzes = [];

            Console.WriteLine("Give the start-time of break:");
            TimeOnly starttime = TimeOnly.Parse(Console.ReadLine());

            Break pauze = new(starttime);
            Pauzes.Add(pauze);

            foreach (var Pauze in Pauzes)
                Console.WriteLine(Pauze);

            return pauze;
        }




    }
}
