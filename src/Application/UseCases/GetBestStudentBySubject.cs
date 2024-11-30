using ifficient_school.src.Application.DTOs;
using ifficient_school.src.Application.Interfaces;

namespace ifficient_school.src.Application.UseCases
{
    public class GetBestStudentBySubject(IStudentRepository repository)
    {
        private readonly IStudentRepository _repository = repository;

        public async Task<IEnumerable<BestStudentBySubjectResponse>> ExecuteAsync()
        {
            var students = await _repository.GetAllAsync();

            var bestBySubject = new List<BestStudentBySubjectResponse>();

            if (students.Any() && students.First().Grades != null && students.First().Grades.Count > 0)
            {
                foreach (var subject in students.First().Grades.Keys)
                {
                   var best = students.OrderByDescending(student => student.Grades.TryGetValue(subject, out var grade) ? grade : 0)
                   .FirstOrDefault();

                    if (best != null)
                    {
                        var bestStudentDto = new StudentDto
                        {
                            Name = best.Name,  
                            Registration = best.Registration  
                        };

                        bestBySubject.Add(new BestStudentBySubjectResponse
                        {
                            Subject = subject,  
                            BestStudent = bestStudentDto, 
                            Grade = best.Grades.TryGetValue(subject, out var grade) ? grade : 0 
                        });
                    }
                }
            }

            return bestBySubject;
        }

    }
}
