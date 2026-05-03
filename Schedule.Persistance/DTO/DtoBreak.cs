namespace Schedule.Persistance.DTO
{
    internal class DtoBreak
    {
        TimeOnly StartTime { get; set; }
        TimeOnly endTime { get; set; }
        int DurationInMinutes { get; set; }
        string catergory { get; set; }
    }
}
