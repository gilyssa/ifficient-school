using ifficient_school.src.Application.Interfaces;
using ifficient_school.src.Application.UseCases;
using ifficient_school.src.Domain.Entities;
using Moq;
using Xunit;

namespace ifficient_school.src.Tests.Application
{
    public class GetSortStudentsByAverageTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldReturnSortedList_WhenBubbleSortStrategyIsUsed()
        {
            var mockRepository = new Mock<IStudentRepository>();
            var students = new List<Student>
            {
                new()
                {
                    Registration = 1,
                    Name = "John",
                    Grades = new Dictionary<string, int> { { "Math", 80 }, { "Science", 90 } },
                },
                new()
                {
                    Registration = 2,
                    Name = "Jane",
                    Grades = new Dictionary<string, int> { { "Math", 85 }, { "Science", 95 } },
                },
                new()
                {
                    Registration = 3,
                    Name = "Bob",
                    Grades = new Dictionary<string, int> { { "Math", 70 }, { "Science", 75 } },
                },
            };

            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(students);
            var useCase = new GetSortStudentsByAverage(mockRepository.Object);

            Func<IEnumerable<Student>, IEnumerable<Student>> bubbleSortStrategy = studentsList =>
                studentsList.OrderByDescending(s => s.Grades.Values.Average()).ToList();

            var sortedStudents = await useCase.ExecuteAsync(bubbleSortStrategy);

            var sortedList = sortedStudents.ToList();
            Assert.Equal(2, sortedList[0].Registration);
            Assert.Equal(1, sortedList[1].Registration);
            Assert.Equal(3, sortedList[2].Registration);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnSortedList_WhenQuickSortStrategyIsUsed()
        {
            var mockRepository = new Mock<IStudentRepository>();
            var students = new List<Student>
            {
                new()
                {
                    Registration = 1,
                    Name = "John",
                    Grades = new Dictionary<string, int> { { "Math", 80 }, { "Science", 90 } },
                },
                new()
                {
                    Registration = 2,
                    Name = "Jane",
                    Grades = new Dictionary<string, int> { { "Math", 85 }, { "Science", 95 } },
                },
                new()
                {
                    Registration = 3,
                    Name = "Bob",
                    Grades = new Dictionary<string, int> { { "Math", 70 }, { "Science", 75 } },
                },
            };

            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(students);
            var useCase = new GetSortStudentsByAverage(mockRepository.Object);

            Func<IEnumerable<Student>, IEnumerable<Student>> quickSortStrategy = studentsList =>
                studentsList.OrderByDescending(s => s.Grades.Values.Average()).ToList();

            var sortedStudents = await useCase.ExecuteAsync(quickSortStrategy);

            var sortedList = sortedStudents.ToList();
            Assert.Equal(2, sortedList[0].Registration);
            Assert.Equal(1, sortedList[1].Registration);
            Assert.Equal(3, sortedList[2].Registration);
        }
    }
}
