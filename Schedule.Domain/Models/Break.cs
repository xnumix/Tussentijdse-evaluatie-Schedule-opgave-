using Schedule.Domain.Repository;

namespace Schedule.Domain.Models
{
    public class Break : Activity
    {
        public Break(TimeOnly startTime, int durationInMinutes) : base(startTime)
        {
            DurationInMinutes = durationInMinutes;

            if (durationInMinutes == 0)
            {
                DurationInMinutes = 60;
            }
        }

        public override TimeOnly EndTime => TimeHelper.CalculateEndTime(StartTime, DurationInMinutes);
        
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

        public override string GetCategory() => "Break";

        public override string? ToString()
        {
            return $"{base.ToString()} - {EndTime} - {GetCategory()} for {DurationInMinutes} minutes";
        }
    }
}
