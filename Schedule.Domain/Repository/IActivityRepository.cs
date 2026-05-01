namespace Schedule.Domain.Repository;

public interface IActivityRepository
{
    List<string> GetActivity();

    List<string> AddNewActivityToPlanning();

}
