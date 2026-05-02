using Schedule.Domain.Repository;

namespace Schedule.Domain.Models
{
    public class Break(TimeOnly startTime) : IPlannableActivity
    {
        public Break(TimeOnly startTime, int durationInMinutes) : this(startTime)
        {
            DurationInMinutes = durationInMinutes;

            if (durationInMinutes == 0)
            {
                DurationInMinutes = 60;
            }
        }

        public TimeOnly StartTime
        {
            get;
        } = startTime;

        public TimeOnly EndTime
        {
            get;
            init
            {
                value = TimeHelper.CalculateEndTime(StartTime, DurationInMinutes);
                field = value;
            }
        }

        public int DurationInMinutes
        {
            get;
            init
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Break must take atleast 1 minute.");
                }
                field = value;
            }
        }

        public int CompareTo(IPlannableActivity? other)
        {
            throw new NotImplementedException();
        }

        public override string? ToString()
        {
            return $"{GetType().Name} for {DurationInMinutes} minutes";
        }
    }
}
