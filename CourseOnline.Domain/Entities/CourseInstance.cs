using CourseOnline.Domain.Shared;

namespace CourseOnline.Domain.Entities
{
    public class CourseInstance : EntityBase
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public Guid InstructorId { get; set; }
        public Instructor Instructor { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}