using ifficient_school.src.Application.Interfaces;
using ifficient_school.src.Application.UseCases;
using ifficient_school.src.Domain.Entities;
using Moq;
using Xunit;

namespace ifficient_school.src.Tests.Application;

public class GetStudentByRegistrationTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnStudent_WhenRegistrationExists()
    {
        var mockRepository = new Mock<IStudentRepository>();
        var expectedStudent = new Student
        {
            Registration = 12345,
            Name = "John Doe",
            Grades = new Dictionary<string, int> { { "Math", 80 }, { "Science", 90 } },
        };

        mockRepository
            .Setup(repo => repo.GetByRegistrationAsync(12345))
            .ReturnsAsync(expectedStudent);

        var useCase = new GetStudentByRegistration(mockRepository.Object);

        var result = await useCase.ExecuteAsync(12345);

        Assert.NotNull(result);
        Assert.Equal(expectedStudent.Name, result?.Name);
        Assert.Equal(expectedStudent.Registration, result?.Registration);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnNull_WhenRegistrationDoesNotExist()
    {
        var mockRepository = new Mock<IStudentRepository>();

        mockRepository
            .Setup(repo => repo.GetByRegistrationAsync(99999))
            .ReturnsAsync((Student?)null);

        var useCase = new GetStudentByRegistration(mockRepository.Object);

        var result = await useCase.ExecuteAsync(99999);

        Assert.Null(result);
    }
}
