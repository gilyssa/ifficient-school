using ifficient_school.src.Domain.Entities;

namespace ifficient_school.src.Application.UseCases
{
    public static class SortingStrategies
    {
        public static IEnumerable<Student> BubbleSort(IEnumerable<Student> students)
        {
            var studentsList = students.ToList();
            int studentCount = studentsList.Count;

            for (int outerIndex = 0; outerIndex < studentCount - 1; outerIndex++)
            {
                for (int innerIndex = 0; innerIndex < studentCount - outerIndex - 1; innerIndex++)
                {
                    var currentStudentAverage = studentsList[innerIndex].Grades.Values.Average();
                    var nextStudentAverage = studentsList[innerIndex + 1].Grades.Values.Average();

                    if (currentStudentAverage < nextStudentAverage)
                    {
                        (studentsList[innerIndex], studentsList[innerIndex + 1]) = (
                            studentsList[innerIndex + 1],
                            studentsList[innerIndex]
                        );
                    }
                }
            }

            return studentsList;
        }

        public static IEnumerable<Student> QuickSort(IEnumerable<Student> students)
        {
            if (!students.Any())
                return [];

            var pivotStudent = students.First();
            var pivotAverage = pivotStudent.Grades.Values.Average();

            var studentsWithLowerOrEqualAverage = students
                .Skip(1)
                .Where(student => student.Grades.Values.Average() > pivotAverage);

            var studentsWithHigherAverage = students
                .Skip(1)
                .Where(student => student.Grades.Values.Average() > pivotAverage);

            return QuickSort(studentsWithLowerOrEqualAverage)
                .Append(pivotStudent)
                .Concat(QuickSort(studentsWithHigherAverage));
        }
    }
}
