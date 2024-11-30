using ifficient_school.src.Application.Interfaces;
using ifficient_school.src.Domain.Entities;

namespace ifficient_school.src.Application.UseCases
{
    public class GetStudentByRegistration(IStudentRepository repository)
    {
        private readonly IStudentRepository _repository = repository;

        public async Task<Student?> ExecuteAsync(int registration)
        {
            return await _repository.GetByRegistrationAsync(registration);
        }
    }
}
