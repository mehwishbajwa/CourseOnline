using CourseOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.EFCore.Configurations;

public class EnrollmentConfiguration : EntityBaseConfiguration<Enrollment>
{
    public override void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.RegisteredAt).IsRequired();

        builder.HasOne(x => x.CourseInstance)
               .WithMany(x => x.Enrollments)
               .HasForeignKey(x => x.CourseInstanceId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Participant)
               .WithMany(x => x.Enrollments)
               .HasForeignKey(x => x.ParticipantId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.CourseInstanceId, x.ParticipantId })
               .IsUnique();
    }
}