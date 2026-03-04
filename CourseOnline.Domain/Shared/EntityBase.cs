namespace CourseOnline.Domain.Shared
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedAt { get; set; }

        public byte[]? Concurrency { get; set; }
    }
}