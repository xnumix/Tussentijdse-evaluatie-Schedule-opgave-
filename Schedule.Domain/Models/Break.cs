using Schedule.Domain.Repository;

namespace Schedule.Domain.Models
{
    public class Break : Activity, IPlannableActivity
    {
        public Break(TimeOnly startTime, int durationInMinutes) : base(startTime)
        {
            DurationInMinutes = durationInMinutes;

            if (durationInMinutes == 0)
            {
                DurationInMinutes = 60;
            }
        }

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
        public override string GetCategory() => "Break";

        public override string? ToString()
        {
            return $"{base.ToString()} - {EndTime}- {GetCategory()} for {DurationInMinutes} minutes";
        }
    }
}
