using CourseOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.EFCore.Configurations;

public class InstructorConfiguration : EntityBaseConfiguration<Instructor>
{
    public override void Configure(EntityTypeBuilder<Instructor> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(200).IsRequired();
        builder.Property(x => x.PhoneNumber).HasMaxLength(30);

        builder.HasIndex(x => x.Email).IsUnique();
    }
}
