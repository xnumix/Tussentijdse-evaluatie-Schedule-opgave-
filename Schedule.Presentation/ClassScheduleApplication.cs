using Schedule.Domain;

namespace Schedule.Presentation
{
    public class ClassScheduleApplication
    {
        private DomainManager domainManager;

        public ClassScheduleApplication(DomainManager domainManager)
        {
            this.domainManager = domainManager;
        }

        public static void StartApplication(DomainManager _domainManager)
        {
            while (true)
            {
                Console.WriteLine("Menu\n1. Add Lesson\n2. Add Excursion\n3. Add Break \n0. Stop\nPick an option");

                int inputNummer = ReadIntBetween(0, 3);
                switch (inputNummer)
                {
                    case 1:
                        AddLesson(_domainManager);
                        break;
                    case 2:
                        AddExcursion(_domainManager);
                        break;
                    case 3:
                        AddBreak(_domainManager);
                        break;
                    case 0:
                        ShutdownApplication(_domainManager);
                        return;
                }
            }
        }
        private static int ReadIntBetween(int min, int max)
        {
            while (true)
            {
                try
                {
                    int input = int.Parse(Console.ReadLine());
                    if (input < min || input > max)
                        throw new ArgumentException($"Choice must be between {min} & {max}");
                    return input;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"ArgumentException {ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"FormatException {ex.Message}");
                }
            }
        }

        private static void ShutdownApplication(DomainManager domainManager)
        {
            List<string> activitiesAsStrings = domainManager.GetActivities();
            if (activitiesAsStrings.Count == 0)
                Console.WriteLine("No Activities on planner");
            else
            {
                Console.WriteLine("Activities on planner:");
                foreach (string s in activitiesAsStrings)
                    Console.WriteLine(s);
            }
            Console.WriteLine("Stopping application...");
            Environment.Exit(0);
        }

        private static void AddLesson(DomainManager _domainManager)
        {
            Console.WriteLine("Pick a starttime for the lesson");
            TimeOnly startTime = TimeOnly.Parse(Console.ReadLine());

            Console.WriteLine("Give the name for the lesson");
            string lessonName = Console.ReadLine();

            Console.WriteLine("Give the studentcount");
            int studentCount = int.Parse(Console.ReadLine());

            Domain.Models.Lesson newLesson = new(startTime, lessonName, studentCount); 
        }

        private static void AddExcursion(DomainManager _domainManager)
        {
            Console.WriteLine("Pick a starttime for the excursion");
            TimeOnly startTime = TimeOnly.Parse(Console.ReadLine());

            Console.WriteLine("Give the name for the excursion");
            string excursionName = Console.ReadLine();

            Console.WriteLine("Give the studentcount");
            int studentCount = int.Parse(Console.ReadLine());

            Console.WriteLine("Give the length of travel in minutes");
            int travelLength=int.Parse(Console.ReadLine());

            Domain.Models.Excursion newExcursion = new(startTime, excursionName, studentCount, travelLength);
        }

        private static void AddBreak(DomainManager _domainManager)
        {
            Console.WriteLine("Pick a starttime for the break");
            TimeOnly startTime = TimeOnly.Parse(Console.ReadLine());

            Console.WriteLine("Give the length of the break");
            int breakCount = int.Parse(Console.ReadLine());

            Domain.Models.Break newBreak = new(startTime, breakCount);
        }

    }
}
