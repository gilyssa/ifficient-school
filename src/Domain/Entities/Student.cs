namespace ifficient_school.src.Domain.Entities
{
    public class Student
    {
        public int Registration { get; set; }
        public required string Name { get; set; }
        public required Dictionary<string, int> Grades { get; set; }

        public bool IsApproved => Grades.Values.All(grade => grade >= 60);
    }
}
