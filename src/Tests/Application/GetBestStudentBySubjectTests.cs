using Moq;
using ifficient_school.src.Application.UseCases;
using ifficient_school.src.Application.Interfaces;
using ifficient_school.src.Domain.Entities;
using Xunit;

namespace ifficient_school.src.Tests.Application;
public class GetBestStudentBySubjectTests
{
    [Fact]
    public async Task ExecuteAsync_ReturnsBestStudentBySubject()
    {
        var students = new List<Student>
        {
            new()
            {
                Registration = 1,
                Name = "John",
                Grades = new Dictionary<string, int>
                {
                    { "Math", 90 },
                    { "English", 80 }
                }
            },
            new ()
            {
                Registration = 2,
                Name = "Jane",
                Grades = new Dictionary<string, int>
                {
                    { "Math", 95 },
                    { "English", 85 }
                }
            }
        };

        var mockRepository = new Mock<IStudentRepository>();
        mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(students);

        var useCase = new GetBestStudentBySubject(mockRepository.Object);

        var result = await useCase.ExecuteAsync();

        var mathResult = result.FirstOrDefault(r => r.Subject == "Math");
        var englishResult = result.FirstOrDefault(r => r.Subject == "English");

        Assert.NotNull(mathResult);
        Assert.NotNull(englishResult);

        Assert.Equal("Jane", mathResult.BestStudent.Name); 
        Assert.Equal("Jane", englishResult.BestStudent.Name);
    }

}