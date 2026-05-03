namespace Schedule.Domain.DTO;

public record DayDto(DateOnly Date, TimeOnly StartTime, TimeOnly EndTime, IReadOnlyList<ActivityDto> Activities);
