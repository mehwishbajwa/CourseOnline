using CourseOnline.Application.Repositories;
using CourseOnline.Application.Services;
using CourseOnline.Application.DTOs;
using CourseOnline.Domain.Entities;

namespace CourseOnline.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repo;

    public CourseService(ICourseRepository repo) => _repo = repo;

    public Task<List<Course>> GetAllAsync(CancellationToken ct = default)
        => _repo.GetAllAsync(ct);

    public Task<Course?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _repo.GetByIdAsync(id, ct);

    public async Task<Course> CreateAsync(CourseCreateDto dto, CancellationToken ct = default)
    {
        var course = new Course
        {
            Title = dto.Title,
            Description = dto.Description
        };

        await _repo.AddAsync(course, ct);
        return course;
    }

    public async Task<Course?> UpdateAsync(Guid id, CourseUpdateDto dto, CancellationToken ct = default)
    {
        var course = await _repo.GetByIdAsync(id, ct);
        if (course is null) return null;

        course.Title = dto.Title;
        course.Description = dto.Description;

        await _repo.UpdateAsync(course, ct);
        return course;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var course = await _repo.GetByIdAsync(id, ct);
        if (course is null) return false;

        await _repo.DeleteAsync(course, ct);
        return true;
    }
}