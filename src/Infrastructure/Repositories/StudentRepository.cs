using ifficient_school.src.Application.Interfaces;
using ifficient_school.src.Domain.Entities;

namespace ifficient_school.src.Infrastructure.Repositories
{
    public class StudentRepository(string filePath) : IStudentRepository
    {
        private readonly string _filePath = filePath;

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var lines = await File.ReadAllLinesAsync(_filePath);
            return lines.Skip(1).Select(ParseStudent);
        }

        public async Task<Student?> GetByRegistrationAsync(int registration)
        {
            var students = await GetAllAsync();
            return students.FirstOrDefault(s => s.Registration == registration);
        }

        private Student ParseStudent(string line)
        {
            var parts = line.Split(',');
            return new Student
            {
                Registration = int.Parse(parts[0]),
                Name = parts[1],
                Grades = new Dictionary<string, int>
                {
                    { "Mathematics", int.Parse(parts[2]) },
                    { "Portuguese", int.Parse(parts[3]) },
                    { "Biology", int.Parse(parts[4]) },
                    { "Chemistry", int.Parse(parts[5]) },
                }
            };
        }
    }
}
