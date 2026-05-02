namespace Schedule.Domain.Repository;

public interface IActivityRepository
{
    List<string> GetActivitiesFromPlanning();

    List<string> AddNewActivityToPlanning();

}
