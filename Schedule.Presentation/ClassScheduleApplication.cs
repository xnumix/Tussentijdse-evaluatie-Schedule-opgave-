using Schedule.Domain;
using Schedule.Domain.DTO;
using Spectre.Console;

namespace Schedule.Presentation
{
    public class ClassScheduleApplication
    {
        private readonly DomainManager _domainManager;

        public ClassScheduleApplication(DomainManager domainManager)
        {
            _domainManager = domainManager;
            StartApplication();
        }

        public void StartApplication()
        {
            AnsiConsole.Write(new FigletText("Schedule").Color(Color.DodgerBlue1));
            AnsiConsole.Write(new Rule().RuleStyle(Style.Parse("blue")));

            AnsiConsole.MarkupLine("[deepskyblue1]First, create a day to start scheduling.[/]");
            CreateDayInteractive(required: true);

            while (true)
            {
                AnsiConsole.WriteLine();
                string choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[blue]MENU[/] [grey](use arrows + enter)[/]")
                        .HighlightStyle(new Style(foreground: Color.White, background: Color.DodgerBlue1))
                        .AddChoices("Add Lesson", "Add Excursion", "Add Break", "Add Day", "Show Day", "Stop"));

                try
                {
                    switch (choice)
                    {
                        case "Add Lesson": AddLesson(); break;
                        case "Add Excursion": AddExcursion(); break;
                        case "Add Break": AddBreak(); break;
                        case "Add Day": CreateDayInteractive(required: false); break;
                        case "Show Day": ShowDay(); break;
                        case "Stop": return;
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Error:[/] {Markup.Escape(ex.Message)}");
                }
            }
        }

        private void CreateDayInteractive(bool required)
        {
            while (true)
            {
                try
                {
                    AnsiConsole.Write(new Rule("[blue]New day[/]").RuleStyle(Style.Parse("blue")).LeftJustified());

                    DateOnly date = AskDate("[deepskyblue1]Date (YYYY-MM-DD):[/]");
                    TimeOnly start = AskTime("[deepskyblue1]Day start time (HH:MM):[/]");
                    TimeOnly end = AskTime("[deepskyblue1]Day end time (HH:MM):[/]");

                    _domainManager.CreateNewDay(date, start, end);
                    AnsiConsole.MarkupLine($"[blue]Day {date} created.[/]");
                    return;
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Error:[/] {Markup.Escape(ex.Message)}");
                    if (!required) return;
                    AnsiConsole.MarkupLine("[deepskyblue1]A day is required to continue. Please try again.[/]");
                }
            }
        }

        private DayDto? PickDay(string title)
        {
            IReadOnlyList<DayDto> days = _domainManager.ListDays();
            if (days.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No days available. Add a day first.[/]");
                return null;
            }

            Dictionary<string, DayDto> byLabel = days.ToDictionary(
                d => $"{d.Date} ({d.StartTime}-{d.EndTime})");

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"[blue]{title}[/]")
                    .HighlightStyle(new Style(foreground: Color.White, background: Color.DodgerBlue1))
                    .AddChoices(byLabel.Keys));

            return byLabel[choice];
        }

        private void AddLesson()
        {
            DayDto? day = PickDay("Pick a day for the lesson");
            if (day is null) return;

            AnsiConsole.Write(new Rule("[blue]New lesson[/]").RuleStyle(Style.Parse("blue")).LeftJustified());

            string lessonName = AskString("[deepskyblue1]Give the name of the lesson:[/]");
            TimeOnly starttime = AskTime("[deepskyblue1]Give the start-time of lesson:[/]");
            int studentCount = AskInt("[deepskyblue1]Give the ammount of students:[/]");

            _domainManager.CreateNewLesson(day.Date, starttime, lessonName, studentCount);
            AnsiConsole.MarkupLine("[blue]Lesson added.[/]");
        }

        private void AddBreak()
        {
            DayDto? day = PickDay("Pick a day for the break");
            if (day is null) return;

            AnsiConsole.Write(new Rule("[blue]New break[/]").RuleStyle(Style.Parse("blue")).LeftJustified());

            TimeOnly starttime = AskTime("[deepskyblue1]Give the start-time of break:[/]");
            int lengthBreak = AskInt("[deepskyblue1]How many minutes is the break:[/]");

            _domainManager.CreateNewBreak(day.Date, starttime, lengthBreak);
            AnsiConsole.MarkupLine("[blue]Break added.[/]");
        }

        private void AddExcursion()
        {
            DayDto? day = PickDay("Pick a day for the excursion");
            if (day is null) return;

            AnsiConsole.Write(new Rule("[blue]New excursion[/]").RuleStyle(Style.Parse("blue")).LeftJustified());

            string excursionName = AskString("[deepskyblue1]Give the name of the excursion:[/]");
            TimeOnly starttime = AskTime("[deepskyblue1]Give the start-time of excursion:[/]");
            int studentCount = AskInt("[deepskyblue1]Give the ammount of students:[/]");
            int travelTime = AskInt("[deepskyblue1]How long will the excursion take?:[/]");

            _domainManager.CreateNewExcursion(day.Date, travelTime, starttime, excursionName, studentCount);
            AnsiConsole.MarkupLine("[blue]Excursion added.[/]");
        }

        private void ShowDay()
        {
            DayDto? day = PickDay("Pick a day to show");
            if (day is null) return;

            AnsiConsole.Write(
                new Rule($"[blue]Day {day.Date}  [grey]({day.StartTime}-{day.EndTime})[/][/]")
                    .RuleStyle(Style.Parse("blue"))
                    .LeftJustified());

            if (day.Activities.Count == 0)
            {
                AnsiConsole.MarkupLine("[grey](empty)[/]");
                return;
            }

            foreach (ActivityDto a in day.Activities)
                AnsiConsole.MarkupLine($"  [deepskyblue1]{Markup.Escape(a.Display)}[/]");
        }

        private static DateOnly AskDate(string prompt)
        {
            string input = AnsiConsole.Prompt(
                new TextPrompt<string>(prompt)
                    .Validate(s => DateOnly.TryParse(s, out _)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Invalid date.[/]")));
            return DateOnly.Parse(input);
        }

        private static TimeOnly AskTime(string prompt)
        {
            string input = AnsiConsole.Prompt(
                new TextPrompt<string>(prompt)
                    .Validate(s => TimeOnly.TryParse(s, out _)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Invalid time.[/]")));
            return TimeOnly.Parse(input);
        }

        private static int AskInt(string prompt)
            => AnsiConsole.Prompt(
                new TextPrompt<int>(prompt)
                    .ValidationErrorMessage("[red]Please enter a valid number.[/]"));

        private static string AskString(string prompt)
            => AnsiConsole.Prompt(
                new TextPrompt<string>(prompt)
                    .Validate(s => !string.IsNullOrWhiteSpace(s)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Value is required.[/]")));
    }
}
