using Schedule.Domain;
using Schedule.Domain.DTO;

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
                Console.Write("What is the starttime of the day (HH:MM)?: ");
                TimeOnly dayStartTime = AskTimeOfDay();

                Console.Write("What is the endtime of the day (HH:MM)?: ");
                TimeOnly dayEndTime = AskTimeOfDay();

                while (true)
                {
                    Console.WriteLine("MENU\nPick an option:\n1. Add Lesson\n2. Add Excursion\n3. Add Break\n0. Stop");

                    string? inputNummer = Console.ReadLine();
                    try
                    {
                        switch (inputNummer)
                        {
                            case "1":
                                AddLesson(dayStartTime, dayEndTime);
                                ListAllActivities();
                                break;
                            case "2":
                                AddExcursion(dayStartTime, dayEndTime);
                                ListAllActivities();
                                break;
                            case "3":
                                AddBreak(dayStartTime, dayEndTime);
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
        }

        private void ListAllActivities()
        {
            IReadOnlyList<ActivityDto> all = _domainManager.ListActivities();
            if (all.Count == 0) { Console.WriteLine("(empty)"); return; }

            foreach (ActivityDto a in all)
                Console.WriteLine(a.Display);
        }

        private void AddLesson(TimeOnly StartDay, TimeOnly EndDay)
        {
            Console.WriteLine("Give the name of the lesson:");
            string lessonName = Console.ReadLine();

            Console.WriteLine("Give the start-time of lesson:");
            TimeOnly starttime = TimeOnly.Parse(Console.ReadLine());

            Console.WriteLine("Give the ammount of students:");
            int studentCount = int.Parse(Console.ReadLine());

            _domainManager.CreateNewLesson(StartDay, EndDay, starttime, lessonName, studentCount);
            Console.WriteLine("Lesson added.");
        }

        private void AddBreak(TimeOnly StartDay, TimeOnly EndDay)
        {
            Console.WriteLine("Give the start-time of break:");
            TimeOnly starttime = TimeOnly.Parse(Console.ReadLine());

            Console.WriteLine("How many minutes is the break:");
            int lengthBreak = int.Parse(Console.ReadLine());

            _domainManager.CreateNewBreak(StartDay, EndDay, starttime, lengthBreak);
            Console.WriteLine("Break added.");
        }

        private void AddExcursion(TimeOnly StartDay, TimeOnly EndDay)
        {
            Console.WriteLine("Give the name of the excursion:");
            string excursionName = Console.ReadLine();

            Console.WriteLine("Give the start-time of excursion:");
            TimeOnly starttime = TimeOnly.Parse(Console.ReadLine());

            Console.WriteLine("Give the ammount of students:");
            int studentCount = int.Parse(Console.ReadLine());

            Console.WriteLine("How long will the excursion take?:");
            int travelTime = int.Parse(Console.ReadLine());

            _domainManager.CreateNewExcursion(StartDay, EndDay, travelTime, starttime, excursionName, studentCount);
            Console.WriteLine("Excursion added.");
        }

        private TimeOnly AskTimeOfDay()
        {
            TimeOnly? time = null;
            while (time is null)
            {
                try
                {
                    time = TimeOnly.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return (TimeOnly)time;
        }
    }
}
