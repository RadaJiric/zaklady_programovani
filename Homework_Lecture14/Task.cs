using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Lecture14
{
    public class Task
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public bool IsCompleted { get; set; }

        public Task(int id, string name, DateTime dueDate, Priority priority)
        {
            ID = id;
            Name = name;
            DueDate = dueDate;
            Priority = priority;
            IsCompleted = false;
        }

        public override string ToString()
        {
            Console.ForegroundColor = IsCompleted ? ConsoleColor.Green : ConsoleColor.White;
            string result = $"[{ID}] {Name} - {DueDate.ToShortDateString()} - {Priority} - {(IsCompleted ? "✔️" : "❌")}";
            Console.ResetColor();
            return result;
        }
    }

    public enum Priority
    {
        Vysoká = 1,
        Střední = 2,
        Nízká = 3
    }
}

