namespace Schedule.Domain.Models
{
    public abstract class Activity
    {
        public TimeOnly StartTime { get; }

        protected Activity(TimeOnly startTime)
        {
            StartTime = startTime;
        }

        // POLYMORFISME: elke subklasse geeft hier zijn eigen invulling aan.
        public abstract string GetCategory();

        public override string ToString()
        => $"[{GetCategory()}] {StartTime}";
    }
}
