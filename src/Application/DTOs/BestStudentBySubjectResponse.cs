namespace ifficient_school.src.Application.DTOs
{
    public class BestStudentBySubjectResponse
    {
        public required string Subject { get; set; }
        public required StudentDto BestStudent { get; set; }
        public int Grade { get; set; }
    }

    public class StudentDto
    {
        public required string Name { get; set; }
        public required int Registration { get; set; }
    }
}
