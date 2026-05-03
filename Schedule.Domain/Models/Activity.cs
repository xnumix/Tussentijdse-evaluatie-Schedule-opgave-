using Schedule.Domain.Repository;
using System.Xml.Linq;

namespace Schedule.Domain.Models
{
    public abstract class Activity : IPlannableActivity
    {
        public TimeOnly StartTime { get; }

        public abstract TimeOnly EndTime { get; }

        protected Activity(TimeOnly startTime)
        {
            StartTime = startTime;
        }

        public abstract string GetCategory();

        public override string ToString()
        => $"{StartTime}";

        public int CompareTo(IPlannableActivity? other)
        {
            if (other == null)
            return 1;
            return StartTime.CompareTo(other.StartTime);
        }
      

    }
}
