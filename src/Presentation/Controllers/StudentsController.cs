using ifficient_school.src.Application.UseCases;
using ifficient_school.src.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ifficient_school.src.Presentation.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController(
        GetApprovedAndFailedStudents approvedAndFailedUseCase,
        GetBestStudentBySubject bestBySubjectUseCase,
        GetStudentByRegistration getByRegistrationUseCase,
        GetSortStudentsByAverage getSortByAverageUseCase,
        GetAllStudents getAllStudents
        ) : ControllerBase       
    {
        private readonly GetApprovedAndFailedStudents _approvedAndFailedUseCase = approvedAndFailedUseCase;
        private readonly GetBestStudentBySubject _bestBySubjectUseCase = bestBySubjectUseCase;
        private readonly GetStudentByRegistration _getByRegistrationUseCase = getByRegistrationUseCase;
        private readonly GetSortStudentsByAverage _getSortByAverageUseCase = getSortByAverageUseCase;
        private readonly GetAllStudents _getAllStudents = getAllStudents;
        [HttpGet("approved-failed")]
        public async Task<IActionResult> GetApprovedAndFailed()
        {
            var result = await _approvedAndFailedUseCase.ExecuteAsync();
            return Ok(result);
        }

        [HttpGet("best-by-subject")]
        public async Task<IActionResult> GetBestBySubject()
        {
            var result = await _bestBySubjectUseCase.ExecuteAsync();
            return Ok(result);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var result = await _getAllStudents.ExecuteAsync();
            return Ok(result);
        }


        [HttpGet("{registration}")]
        public async Task<IActionResult> GetByRegistration(int registration)
        {
            var student = await _getByRegistrationUseCase.ExecuteAsync(registration);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpGet("sort")]
        public async Task<IActionResult> SortStudents([FromQuery] string strategy = "bubble")
        {
            return strategy.ToLower() switch
            {
                "bubble" => Ok(await _getSortByAverageUseCase.ExecuteAsync(SortingStrategies.BubbleSort)),
                "quick" => Ok(await _getSortByAverageUseCase.ExecuteAsync(SortingStrategies.QuickSort)),
                _ => BadRequest("Invalid sorting strategy. Use 'bubble' or 'quick' as query parameter."),
            };
        }

    }
}
