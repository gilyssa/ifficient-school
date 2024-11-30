using ifficient_school.src.Domain.Entities;

namespace ifficient_school.src.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByRegistrationAsync(int registration);
    }
}
