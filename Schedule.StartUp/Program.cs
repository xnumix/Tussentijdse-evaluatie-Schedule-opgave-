
using Microsoft.Extensions.Configuration;
using Schedule.Domain;
using Schedule.Domain.Repository;
using Schedule.Persistance;
using Schedule.Persistance.Overig;
using Schedule.Presentation;


IConfiguration configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

string connectionString = configuration["ConnectionStrings:Schedule"]
    ?? throw new InvalidOperationException("Missing user secret 'ConnectionStrings:Schedule'.");

DbInitializer.Initialize(connectionString);

IDayRepository repository = new DayRepository(connectionString);
DomainManager domainManager = new DomainManager(repository);
ClassScheduleApplication application = new(domainManager);

public partial class Program;
