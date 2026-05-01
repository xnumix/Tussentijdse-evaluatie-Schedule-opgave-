
using Schedule.Domain;
using Schedule.Domain.Repository;
using Schedule.Persistance;
using Schedule.Presentation;


IActivityRepository repository = new ActivityRepository();
DomainManager domainManager = new DomainManager(repository);
ClassScheduleApplication application = new(domainManager);
