using CourseOnline.Domain.Shared;

namespace CourseOnline.Domain.Entities
{
    public class Instructor : EntityBase
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public ICollection<CourseInstance> CourseInstances { get; set; } = new List<CourseInstance>();
    }
}