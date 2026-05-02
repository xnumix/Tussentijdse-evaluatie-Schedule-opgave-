using Schedule.Domain.Repository;

namespace Schedule.Persistance
{
    public class ActivityRepository : IActivityRepository
    {
        public List<string> AddNewActivityToPlanning()
        {
            throw new NotImplementedException();
        }

        public List<string> GetActivitiesFromPlanning()
        {
            throw new NotImplementedException();
        }
    }
}
/* private readonly HashSet<IFuelableAircraft> _iAircrafts = [];

    public void AddToPlanner(IFuelableAircraft iAircraft)
    {
        _iAircrafts.Add(iAircraft);
    }

    public List<IFuelableAircraft> GetAircrafts()
    {
        return _iAircrafts.ToList();
    }
*/