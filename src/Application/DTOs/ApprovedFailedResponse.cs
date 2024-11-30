using ifficient_school.src.Domain.Entities;

namespace ifficient_school.src.Application.DTOs
{
    public class ApprovedFailedResponse
    {
        public required IEnumerable<Student> Approved { get; set; }
        public required IEnumerable<Student> Failed { get; set; }
    }

}
