using Schedule.Domain.Models;
using Schedule.Domain.Repository;

namespace Schedule.Persistance
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly List<Activity> _activities = [];

        public void StoreActivity(Activity activity) => _activities.Add(activity);

        public IReadOnlyList<Activity> GetActivities() => _activities.AsReadOnly();
    }
}
