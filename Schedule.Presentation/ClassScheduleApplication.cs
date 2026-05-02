using Schedule.Domain;

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
                Console.WriteLine("MENU\nPick an option:\n1. Add Lesson\n2. Add Excursion\n3. Add Break \n0. Stop");
                int inputNummer = int.Parse(Console.ReadLine());

                switch (inputNummer)
                {
                    case 1:
                        AddLesson();
                            break;
                    case 2:
                        Console.WriteLine("Test");
                        break;
                    case 3:
                        AddBreak();
                        break;
                    case 0:
                        Console.WriteLine("Test");
                        return;
                }
            }
        }


        private void AddLesson()
        {
            _domainManager.CreateNewLesson();
        }

        private void AddBreak()
        {
            _domainManager.CreateNewBreak();
        }

        
        //    private static void AddExcursion(DomainManager _domainManager)
        //    {
        //        Console.WriteLine("Pick a starttime for the excursion");
        //        TimeOnly startTime = TimeOnly.Parse(Console.ReadLine());

        //        Console.WriteLine("Give the name for the excursion");
        //        string excursionName = Console.ReadLine();

        //        Console.WriteLine("Give the studentcount");
        //        int studentCount = int.Parse(Console.ReadLine());

        //        Console.WriteLine("Give the length of travel in minutes");
        //        int travelLength=int.Parse(Console.ReadLine());

        //        Domain.Models.Excursion newExcursion = new(startTime, excursionName, studentCount, travelLength);
        //    }

        //    private static void AddBreak()
        //    {
        //        Console.WriteLine("Pick a starttime for the break");
        //        TimeOnly startTime = TimeOnly.Parse(Console.ReadLine());

        //        Console.WriteLine("Give the length of the break");
        //        int breakCount = int.Parse(Console.ReadLine());

        //        Break newBreak = new(startTime, breakCount);
        //    }

    }
}
