using CourseOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.EFCore.Configurations;

public class CourseConfiguration : EntityBaseConfiguration<Course>
{
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Title)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(x => x.Description)
               .HasMaxLength(2000);

        builder.HasIndex(x => x.Title);
    }
}