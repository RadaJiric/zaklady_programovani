using HomeworkLecture10;

namespace Homework_lecture10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var random = new Random();
            var names = new List<string> { "Alice", "Bob", "Charlie", "Diana", "Edward", "Fiona", "George", "Hannah", "Ian", "Julia" };
            var students = new List<Student>();

            for (int i = 0; i < 100; i++)
            {
                var student = new Student
                {
                    Name = names[random.Next(names.Count)] + i,
                    Age = random.Next(18, 25),
                    Grades = new List<int> { random.Next(20, 100), random.Next(50, 100), random.Next(70, 100) }
                };
                students.Add(student);
            }

            
            var studentNames = students.Select(s => s.Name).ToList();
            Console.WriteLine("Seznam jmen studentů:");
            studentNames.ForEach(name => Console.WriteLine(name));

            
            var highGradeStudents = students.Where(s => s.Grades.Any(g => g > 90)).ToList();
            Console.WriteLine("\nStudenti s alespoň jednou známkou vyšší než 90:");
            highGradeStudents.ForEach(s => Console.WriteLine(s.Name));

            
            var allGradesAbove80 = students.Any(s => s.Grades.All(g => g > 80));
            Console.WriteLine($"\nExistuje student, který má všechny známky vyšší než 80: {allGradesAbove80}");

            
            var allGrades = students.SelectMany(s => s.Grades).ToList();
            Console.WriteLine("\nSeznam všech známek:");
            allGrades.ForEach(grade => Console.WriteLine(grade));

            
            var sortedByAge = students.OrderBy(s => s.Age).ToList();
            Console.WriteLine("\nStudenti seřazení podle věku:");
            sortedByAge.ForEach(s => Console.WriteLine($"{s.Name}, Věk: {s.Age}"));
        }
    }
}

        
    

