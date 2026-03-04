using CourseOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.EFCore.Configurations;

public class CourseInstanceConfiguration : EntityBaseConfiguration<CourseInstance>
{
    public override void Configure(EntityTypeBuilder<CourseInstance> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();

        builder.HasOne(x => x.Course)
               .WithMany(x => x.Instances)
               .HasForeignKey(x => x.CourseId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Instructor)
               .WithMany(x => x.CourseInstances)
               .HasForeignKey(x => x.InstructorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CourseId, x.StartDate });
    }
}