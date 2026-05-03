using Schedule.Domain.Repository;

namespace Schedule.Domain.Models;

public class Day
{
    private readonly List<IPlannableActivity> _activities = [];

    public Day(DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        if (endTime <= startTime)
            throw new ArgumentException("End time must be after start time.", nameof(endTime));

        Date = date;
        StartTime = startTime;
        EndTime = endTime;
    }

    public DateOnly Date { get; }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }
    public IReadOnlyList<IPlannableActivity> Activities => _activities.AsReadOnly();

    public void AddActivity(Activity activity)
    {
        if (!TimeHelper.FitsInSchedule(activity, StartTime, EndTime))
            throw new OutOfScheduleRangeException("Activity does not fit in this day's schedule.", StartTime, EndTime);

        TimeHelper.ValidateNoOverlaps(activity, _activities);
        _activities.Add(activity);
        _activities.Sort();
    }
}
