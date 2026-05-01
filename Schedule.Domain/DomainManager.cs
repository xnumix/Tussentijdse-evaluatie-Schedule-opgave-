using Schedule.Domain.Models;
using Schedule.Domain.Repository;

namespace Schedule.Domain
{
    public class DomainManager
    {
        private IActivityRepository repository;

        public DomainManager(IPlannableActivity plannableActivity)
        {

        }

        public DomainManager(IActivityRepository repository)
        {
            this.repository = repository;
        }

        public List<string> GetActivities()
        {
            throw new NotImplementedException();
        }
    }
}
/*     public DomainManager(IDestinationRepository destinations)
        {
            _destinations = destinations;
            _planner = new AircraftPlanner();
        }
*/