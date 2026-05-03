using Schedule.Domain;
using Schedule.Domain.Models;
using Schedule.Domain.Repository;

namespace Schedule.Presentation
{
    public class ClassScheduleApplication
    {
        // Dependency injection van de domain laag
        private readonly DomainManager _domainManager;

        public ClassScheduleApplication(DomainManager domainManager)
        {
            _domainManager = domainManager;
            StartApplication();
        }

        public void StartApplication()
        {
            while (true)
            {
                Console.WriteLine("MENU\nPick an option:\n1. Add Lesson\n2. Add Excursion\n3. Add Break\n0. Stop");

                string? inputNummer = Console.ReadLine();
                try
                {
                    switch (inputNummer)
                    {
                        case "1":
                            AddLesson();
                            ListAllActivities();
                            break;
                        case "2":
                            AddExcursion();
                            ListAllActivities();
                            break;
                        case "3":
                            AddBreak();
                            ListAllActivities();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private void ListAllActivities()
        {
            IReadOnlyList<IPlannableActivity> all = _domainManager.ListActivities();
            if (all.Count == 0) { Console.WriteLine("(empty)"); return; }

            // POLYMORFISME in actie: elk Activity roept zijn eigen ToString aan,
            // de presentation-laag weet niet of het een lesson, break of excursion is.
            foreach (Activity a in all)
                Console.WriteLine(a);
        }

        private void AddLesson()
        {
            Console.WriteLine("Give the name of the lesson:");
            string lessonName = Console.ReadLine();

            Console.WriteLine("Give the start-time of lesson:");
            TimeOnly starttime = TimeOnly.Parse(Console.ReadLine());

            Console.WriteLine("Give the ammount of students:");
            int studentCount = int.Parse(Console.ReadLine());

            _domainManager.CreateNewLesson(starttime, lessonName, studentCount);
            Console.WriteLine("Lesson added.");
        }

        private void AddBreak()
        {
            Console.WriteLine("Give the start-time of break:");
            TimeOnly starttime = TimeOnly.Parse(Console.ReadLine());

            Console.WriteLine("How many minutes is the break:");
            int lengthBreak = int.Parse(Console.ReadLine());

            _domainManager.CreateNewBreak(starttime, lengthBreak);
            Console.WriteLine("Break added.");
        }

        private void AddExcursion()
        {
            Console.WriteLine("Give the name of the excursion:");
            string excursionName = Console.ReadLine();

            Console.WriteLine("Give the start-time of excursion:");
            TimeOnly starttime = TimeOnly.Parse(Console.ReadLine());

            Console.WriteLine("Give the ammount of students:");
            int studentCount = int.Parse(Console.ReadLine());

            Console.WriteLine("How long will the excursion take?:");
            int travelTime = int.Parse(Console.ReadLine());

            _domainManager.CreateNewExcursion(travelTime, starttime, excursionName, studentCount);
            Console.WriteLine("Excursion added.");
        }
    }
}
