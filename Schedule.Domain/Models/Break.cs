using Schedule.Domain.Repository;

namespace Schedule.Domain.Models
{
    public class Break : IPlannableActivity
    {
        private TimeOnly _startTime;
        private TimeOnly _endTime;
        private int _durationInMinutes;

        public Break(TimeOnly startTime)
        {
            _startTime = startTime;
        }

        public Break(TimeOnly startTime, int durationInMinutes) : this(startTime)
        {
            DurationInMinutes = durationInMinutes;

            if (durationInMinutes==0) 
            {
                DurationInMinutes = 60;
            }
        }

        public TimeOnly StartTime
        {
            get => _startTime;
        }

        public TimeOnly EndTime
        {

            get => _endTime;
            init
            {
                value = Domain.Models.TimeHelper.CalculateEndTime(StartTime, DurationInMinutes);
                _endTime = value;
            }
        }

        public int DurationInMinutes
        {
            get => _durationInMinutes;
            init
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Break must take at least 1 minute.");
                }
                _durationInMinutes = value;
            }
        }

        TimeOnly IPlannableActivity.StartTime { get => StartTime; init => throw new NotImplementedException(); }

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
