namespace Schedule.Domain.DTO;

public record ActivityDto(TimeOnly StartTime, TimeOnly EndTime, string Display);
