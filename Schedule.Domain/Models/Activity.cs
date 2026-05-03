namespace Schedule.Domain.Models
{
    public abstract class Activity
    {
        public TimeOnly StartTime { get; }

        protected Activity(TimeOnly startTime)
        {
            StartTime = startTime;
        }

        public abstract string GetCategory();

        public override string ToString()
        => $"{StartTime}";
    }
}
