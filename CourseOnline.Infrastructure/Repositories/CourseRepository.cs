using CourseOnline.Application.Repositories;
using CourseOnline.Domain.Entities;
using CourseOnline.Infrastructure.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Infrastructure.Repositories;

public sealed class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _db;

    public CourseRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Course>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _db.Courses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Course course, CancellationToken cancellationToken = default)
    {
        await _db.Courses.AddAsync(course, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Course course, CancellationToken cancellationToken = default)
    {
        _db.Courses.Update(course);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Course course, CancellationToken cancellationToken = default)
    {
        _db.Courses.Remove(course);
        await _db.SaveChangesAsync(cancellationToken);
    }
}