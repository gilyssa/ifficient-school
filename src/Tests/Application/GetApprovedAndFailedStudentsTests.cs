using ifficient_school.src.Application.UseCases;
using ifficient_school.src.Application.Interfaces;
using ifficient_school.src.Domain.Entities;
using Moq;
using Xunit;

namespace ifficient_school.src.Tests.Application
{
    public class GetApprovedAndFailedStudentsTests
    {
        [Fact]
        public async Task ExecuteAsync_ReturnsApprovedAndFailedStudents()
        {
            var mockRepository = new Mock<IStudentRepository>();
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestStudents());

            var useCase = new GetApprovedAndFailedStudents(mockRepository.Object);

            var result = await useCase.ExecuteAsync();

            Assert.NotNull(result); 
            Assert.True(result.Approved.Any());
            Assert.True(result.Failed.Any());
        }

        private IEnumerable<Student> GetTestStudents()
        {
            return
            [
                new()
                {
                    Registration = 1,
                    Name = "John",
                    Grades = new Dictionary<string, int>
                    {
                        { "Mathematics", 75 },
                        { "Portuguese", 80 },
                        { "Biology", 65 },
                        { "Chemistry", 70 },
                    }
                },
                new()
                {
                    Registration = 2,
                    Name = "Jane",
                    Grades = new Dictionary<string, int>
                    {
                        { "Mathematics", 50 },
                        { "Portuguese", 45 },
                        { "Biology", 55 },
                        { "Chemistry", 60 },
                    }
                },
                new()
                {
                    Registration = 3,
                    Name = "Alex",
                    Grades = new Dictionary<string, int>
                    {
                        { "Mathematics", 40 },
                        { "Portuguese", 30 },
                        { "Biology", 50 },
                        { "Chemistry", 20 },
                    }
                }
            ];
        }

        [Fact]
        public async Task ExecuteAsync_ReturnsEmptyLists_WhenNoStudentsExist()
        {
            var mockRepository = new Mock<IStudentRepository>();
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Student>());
        
            var useCase = new GetApprovedAndFailedStudents(mockRepository.Object);
        
            var result = await useCase.ExecuteAsync();
        
            Assert.NotNull(result);
            Assert.Empty(result.Approved);
            Assert.Empty(result.Failed);
        }
    
        }
}
