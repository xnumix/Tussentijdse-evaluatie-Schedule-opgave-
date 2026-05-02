using Schedule.Domain.Models;
using Schedule.Domain.Repository;

namespace Schedule.Domain
{
    public class DomainManager
    {
        public DomainManager(IActivityRepository repository)
        {
            Repository = repository;
        }

        public IActivityRepository Repository { get; }
    }
}
