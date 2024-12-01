using ifficient_school.src.Application.UseCases;
using ifficient_school.src.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ifficient_school.src.Presentation.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController(
        GetApprovedAndFailedStudents approvedAndFailedUseCase,
        GetBestStudentBySubject bestBySubjectUseCase,
        GetStudentByRegistration getByRegistrationUseCase,
        GetSortStudentsByAverage getSortByAverageUseCase,
        GetAllStudents getAllStudents) : ControllerBase
    {
        private readonly GetApprovedAndFailedStudents _approvedAndFailedUseCase = approvedAndFailedUseCase;
        private readonly GetBestStudentBySubject _bestBySubjectUseCase = bestBySubjectUseCase;
        private readonly GetStudentByRegistration _getByRegistrationUseCase = getByRegistrationUseCase;
        private readonly GetSortStudentsByAverage _getSortByAverageUseCase = getSortByAverageUseCase;
        private readonly GetAllStudents _getAllStudents = getAllStudents;

        [HttpGet("approved-failed")]
        [SwaggerOperation(Summary = "Retrieve approved and failed students.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Description = "Returns a list of approved and failed students based on their grades.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error.")]
        public async Task<IActionResult> GetApprovedAndFailed()
        {
            var result = await _approvedAndFailedUseCase.ExecuteAsync();
            return Ok(result);
        }

        [HttpGet("best-by-subject")]
        [SwaggerOperation(Summary = "Retrieve the best student by subject.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Returns the top student in each subject based on grades.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error.")]
        public async Task<IActionResult> GetBestBySubject()
        {
            var result = await _bestBySubjectUseCase.ExecuteAsync();
            return Ok(result);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieve all students.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Returns a list of all students registered in the system.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error.")]
        public async Task<IActionResult> GetStudents()
        {
            var result = await _getAllStudents.ExecuteAsync();
            return Ok(result);
        }

        [HttpGet("{registration}")]
        [SwaggerOperation(Summary = "Retrieve a student by registration number.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Fetches a specific student's details using their registration number.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Student not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error.")]
        public async Task<IActionResult> GetByRegistration(int registration)
        {
            var student = await _getByRegistrationUseCase.ExecuteAsync(registration);
            if (student == null) return NotFound("Student not found.");
            return Ok(student);
        }

        [HttpGet("sort")]
        [SwaggerOperation(Summary = "Sort students by average grade.")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Sorts students based on their average grade using the specified strategy (bubble or quick sort).")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid sorting strategy.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error.")]
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
