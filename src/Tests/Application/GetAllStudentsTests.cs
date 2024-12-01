using Moq;
using Xunit;
using ifficient_school.src.Application.UseCases;
using ifficient_school.src.Application.Interfaces;
using ifficient_school.src.Domain.Entities;

namespace ifficient_school.src.Tests.Application
{
    public class GetAllStudentsTests
    {
        private readonly Mock<IStudentRepository> _studentRepositoryMock;
        private readonly GetAllStudents _useCase;

        public GetAllStudentsTests()
        {
            _studentRepositoryMock = new Mock<IStudentRepository>();
            _useCase = new GetAllStudents(_studentRepositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnStudents_WhenDataIsValid()
        {
            var students = new List<Student>
            {
                new()
                {
                    Registration = 12345,
                    Name = "John Doe",
                    Grades = new Dictionary<string, int>
                    {
                        { "Mathematics", 70 },
                        { "Portuguese", 80 },
                        { "Biology", 85 },
                        { "Chemistry", 90 }
                    }
                },
                new()
                {
                    Registration = 67890,
                    Name = "Jane Doe",
                    Grades = new Dictionary<string, int>
                    {
                        { "Mathematics", 50 },
                        { "Portuguese", 55 },
                        { "Biology", 60 },
                        { "Chemistry", 65 }
                    }
                }
            };

            _studentRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(students);

            var result = await _useCase.ExecuteAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, s => s.Name == "John Doe");
            Assert.Contains(result, s => s.Name == "Jane Doe");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmpty_WhenFileIsEmpty()
        {
            var emptyList = new List<Student>();

            _studentRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(emptyList);

            var result = await _useCase.ExecuteAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenFileIsNotReadable()
        {
            _studentRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new IOException("File not found"));

            var exception = await Assert.ThrowsAsync<IOException>(() => _useCase.ExecuteAsync());
            Assert.Equal("File not found", exception.Message);
        }
    }
}
