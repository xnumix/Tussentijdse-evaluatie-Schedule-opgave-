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
            Console.WriteLine("First, create a day to start scheduling.");
            CreateDayInteractive(required: true);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("MENU\nPick an option:\n1. Add Lesson\n2. Add Excursion\n3. Add Break\n4. Add Day\n5. Show Day\n0. Stop");

                string? input = Console.ReadLine();
                try
                {
                    switch (input)
                    {
                        case "1": AddLesson(); break;
                        case "2": AddExcursion(); break;
                        case "3": AddBreak(); break;
                        case "4": CreateDayInteractive(required: false); break;
                        case "5": ShowDay(); break;
                        case "0": return;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private void CreateDayInteractive(bool required)
        {
            while (true)
            {
                try
                {
                    Console.Write("Date (YYYY-MM-DD): ");
                    DateOnly date = AskDate();

                    Console.Write("Day start time (HH:MM): ");
                    TimeOnly start = AskTimeOfDay();

                    Console.Write("Day end time (HH:MM): ");
                    TimeOnly end = AskTimeOfDay();

                    _domainManager.CreateNewDay(date, start, end);
                    Console.WriteLine($"Day {date} created.");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    if (!required) return;
                    Console.WriteLine("A day is required to continue. Please try again.");
                }
            }
        }

        private DayDto? PickDay()
        {
            IReadOnlyList<DayDto> days = _domainManager.ListDays();
            if (days.Count == 0)
            {
                Console.WriteLine("No days available. Add a day first.");
                return null;
            }

            Console.WriteLine("Available days:");
            for (int i = 0; i < days.Count; i++)
                Console.WriteLine($"{i + 1}. {days[i].Date} ({days[i].StartTime}-{days[i].EndTime})");

            Console.Write("Pick a day number: ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > days.Count)
            {
                Console.WriteLine("Invalid selection.");
                return null;
            }
            return days[idx - 1];
        }

        private void AddLesson()
        {
            DayDto? day = PickDay();
            if (day is null) return;

            Console.WriteLine("Give the name of the lesson:");
            string? lessonName = Console.ReadLine();

            Console.WriteLine("Give the start-time of lesson:");
            TimeOnly starttime = AskTimeOfDay();

            Console.WriteLine("Give the ammount of students:");
            int studentCount = int.Parse(Console.ReadLine() ?? "0");

            _domainManager.CreateNewLesson(day.Date, starttime, lessonName ?? string.Empty, studentCount);
            Console.WriteLine("Lesson added.");
        }

        private void AddBreak()
        {
            DayDto? day = PickDay();
            if (day is null) return;

            Console.WriteLine("Give the start-time of break:");
            TimeOnly starttime = AskTimeOfDay();

            Console.WriteLine("How many minutes is the break:");
            int lengthBreak = int.Parse(Console.ReadLine() ?? "0");

            _domainManager.CreateNewBreak(day.Date, starttime, lengthBreak);
            Console.WriteLine("Break added.");
        }

        private void AddExcursion()
        {
            DayDto? day = PickDay();
            if (day is null) return;

            Console.WriteLine("Give the name of the excursion:");
            string? excursionName = Console.ReadLine();

            Console.WriteLine("Give the start-time of excursion:");
            TimeOnly starttime = AskTimeOfDay();

            Console.WriteLine("Give the ammount of students:");
            int studentCount = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("How long will the excursion take?:");
            int travelTime = int.Parse(Console.ReadLine() ?? "0");

            _domainManager.CreateNewExcursion(day.Date, travelTime, starttime, excursionName ?? string.Empty, studentCount);
            Console.WriteLine("Excursion added.");
        }

        private void ShowDay()
        {
            DayDto? day = PickDay();
            if (day is null) return;

            Console.WriteLine($"Day {day.Date} ({day.StartTime}-{day.EndTime})");
            if (day.Activities.Count == 0)
            {
                Console.WriteLine("(empty)");
                return;
            }
            foreach (ActivityDto a in day.Activities)
                Console.WriteLine(a.Display);
        }

        private TimeOnly AskTimeOfDay()
        {
            TimeOnly? time = null;
            while (time is null)
            {
                try
                {
                    time = TimeOnly.Parse(Console.ReadLine() ?? string.Empty);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return (TimeOnly)time;
        }

        private DateOnly AskDate()
        {
            DateOnly? date = null;
            while (date is null)
            {
                try
                {
                    date = DateOnly.Parse(Console.ReadLine() ?? string.Empty);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return (DateOnly)date;
        }
    }
}
