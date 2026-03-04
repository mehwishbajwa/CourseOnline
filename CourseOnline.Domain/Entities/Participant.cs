using CourseOnline.Domain.Shared;

namespace CourseOnline.Domain.Entities
{
    public class Participant : EntityBase
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}