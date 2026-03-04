using CourseOnline.Application.DTOs;
using CourseOnline.Domain.Entities;

namespace CourseOnline.Application.Services;

public interface ICourseService
{
    Task<List<Course>> GetAllAsync(CancellationToken ct = default);
    Task<Course?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Course> CreateAsync(CourseCreateDto dto, CancellationToken ct = default);
    Task<Course?> UpdateAsync(Guid id, CourseUpdateDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}