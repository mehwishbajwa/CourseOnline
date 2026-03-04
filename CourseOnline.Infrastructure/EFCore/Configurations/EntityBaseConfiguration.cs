using CourseOnline.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseOnline.Infrastructure.EFCore.Configurations;

public abstract class EntityBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityBase
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt)
               .IsRequired();

        builder.Property(x => x.ModifiedAt)
               .IsRequired(false);

        builder.Property(x => x.Concurrency)
               .IsConcurrencyToken()
               .IsRowVersion();
    }
}