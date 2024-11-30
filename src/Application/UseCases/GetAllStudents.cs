using ifficient_school.src.Application.Interfaces;
using ifficient_school.src.Domain.Entities;

namespace ifficient_school.src.Application.UseCases
{
    public class GetAllStudents(IStudentRepository repository)
    {
        private readonly IStudentRepository _repository = repository;

        public async Task< IEnumerable<Student>?> ExecuteAsync()
        {
            var students = await _repository.GetAllAsync();
    
            return students;
        }
    }
}
