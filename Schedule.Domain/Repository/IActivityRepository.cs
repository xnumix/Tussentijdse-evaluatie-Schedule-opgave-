using Schedule.Domain.Models;

namespace Schedule.Domain.Repository;

public interface IActivityRepository
{
    void StoreActivity(Activity activity);
    IReadOnlyList<IPlannableActivity> GetActivities();
}
