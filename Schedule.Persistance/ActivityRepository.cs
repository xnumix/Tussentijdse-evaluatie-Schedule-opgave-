using Schedule.Domain.Models;
using Schedule.Domain.Repository;

namespace Schedule.Persistance
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly List<IPlannableActivity> _activities = [];

        public void StoreActivity(Activity activity)
        {
            TimeHelper.ValidateNoOverlaps(activity,_activities);
            _activities.Add(activity);
        }
        public IReadOnlyList<IPlannableActivity> GetActivities() => _activities.AsReadOnly();
    }
}
