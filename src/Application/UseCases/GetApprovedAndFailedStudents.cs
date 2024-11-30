using ifficient_school.src.Application.DTOs;
using ifficient_school.src.Application.Interfaces;
using ifficient_school.src.Domain.Entities;

namespace ifficient_school.src.Application.UseCases
{
    public class GetApprovedAndFailedStudents(IStudentRepository repository)
    {
        private readonly IStudentRepository _repository = repository;

        public async Task<ApprovedFailedResponse> ExecuteAsync()
        {
            var students = await _repository.GetAllAsync();
            var approved = students.Where(student => student.IsApproved);
            var failed = students.Where(student => !student.IsApproved);
    
            return new ApprovedFailedResponse
            {
                Approved = approved.ToList(),
                Failed = failed.ToList()
            };
        }
    }
}
