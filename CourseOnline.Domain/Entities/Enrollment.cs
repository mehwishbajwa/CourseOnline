using CourseOnline.Domain.Shared;

namespace CourseOnline.Domain.Entities
{
    public class Enrollment : EntityBase
    {
        public Guid CourseInstanceId { get; set; }

        public CourseInstance CourseInstance { get; set; } = null!;

        public Guid ParticipantId { get; set; }

        public Participant Participant { get; set; } = null!;

        public DateTime RegisteredAt { get; set; }
    }
}