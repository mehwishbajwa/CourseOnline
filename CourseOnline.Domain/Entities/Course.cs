using CourseOnline.Domain.Shared;

namespace CourseOnline.Domain.Entities
{
    public class Course : EntityBase
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<CourseInstance> Instances { get; set; } = new List<CourseInstance>();
    }
}