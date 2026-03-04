namespace CourseOnline.Application.DTOs;

public record CourseCreateDto(string Title, string? Description);
public record CourseUpdateDto(string Title, string? Description);
