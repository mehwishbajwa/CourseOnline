using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CourseOnline.Infrastructure.EFCore.Contexts;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // SQLite for this project (creates courseonline.db)
        optionsBuilder.UseSqlite("Data Source=courseonline.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}