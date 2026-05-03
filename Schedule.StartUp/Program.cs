
using Schedule.Domain;
using Schedule.Domain.Repository;
using Schedule.Persistance;
using Schedule.Presentation;


IDayRepository repository = new DayRepository();
DomainManager domainManager = new DomainManager(repository);
ClassScheduleApplication application = new(domainManager);
