using ifficient_school.src.Application.Interfaces;
using ifficient_school.src.Domain.Entities;
using ifficient_school.src.Domain.Exceptions;

namespace ifficient_school.src.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _filePath;

        public StudentRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException(
                    $"The file '{_filePath}' was not found. Please check the path and ensure the file exists."
                );
            }

            if (new FileInfo(_filePath).Length <= 1)
            {
                throw new CustomException(
                    $"The file '{_filePath}' is empty. Please provide a valid CSV file with data."
                );
            }
        }

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
            if (parts.Length != 6)
            {
                throw new CustomException(
                    $"Invalid line. Expected 6 parts but found {parts.Length}. Line: {line}. Please check the CSV file and ensure all rows are properly formatted."
                );
            }

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
                },
            };
        }
    }
}
