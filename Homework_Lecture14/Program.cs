



    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;

namespace Homework_Lecture14
{

    class Program
    {
        static string filePath = "tasks.json";

        static void Main(string[] args)
        {
            List<Task> tasks = LoadTasks();
            bool running = true;

            while (running)
            {
                Console.Clear();
                DisplayTasks(tasks);
                DisplayMenu();
                var choice = ReadChoice();

                switch (choice)
                {
                    case 1:
                        AddTask(tasks);
                        break;
                    case 2:
                        DeleteTask(tasks);
                        break;
                    case 3:
                        MarkTaskAsCompleted(tasks);
                        break;
                    case 4:
                        EditTask(tasks);
                        break;
                    case 5:
                        DeleteCompletedTasks(tasks);
                        break;
                    case 6:
                        DeleteAllTasks(tasks);
                        break;
                    case 7:
                        running = false;
                        SaveTasks(tasks);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNeplatná volba. Zkuste to znovu.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        static void EditTask(List<Task> tasks)
        {
            Console.Clear();
            Console.Write("Zadejte ID úkolu k úpravě: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var task = tasks.FirstOrDefault(t => t.ID == id);
                if (task != null)
                {
                    Console.Write("Zadejte nový název úkolu: ");
                    task.Name = Console.ReadLine();

                    DateTime dueDate;
                    while (true)
                    {
                        Console.Write("Zadejte nové datum dokončení (YYYY-MM-DD): ");
                        if (DateTime.TryParse(Console.ReadLine(), out dueDate))
                        {
                            task.DueDate = dueDate;
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Neplatný formát data. Zkuste to znovu.");
                        Console.ResetColor();
                    }

                    Priority priority;
                    while (true)
                    {
                        Console.Write("Zadejte novou prioritu (1 = Vysoká, 2 = Střední, 3 = Nízká): ");
                        if (int.TryParse(Console.ReadLine(), out int p) && Enum.IsDefined(typeof(Priority), p))
                        {
                            task.Priority = (Priority)p;
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Neplatná priorita. Zadejte číslo mezi 1 a 3.");
                        Console.ResetColor();
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Úkol byl úspěšně upraven.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Úkol s daným ID neexistuje.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Neplatné ID.");
                Console.ResetColor();
            }
            Console.ReadKey();
        }

        static int ReadChoice()
        {
            while (true)
            {
                Console.Write("Zadejte volbu (1-7): ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 7)
                {
                    return choice;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Neplatný vstup. Zadejte číslo mezi 1 a 7.");
                Console.ResetColor();
            }
        }

        static List<Task> LoadTasks()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
            }
            return new List<Task>();
        }

        static void SaveTasks(List<Task> tasks)
        {
            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(filePath, json);
        }

        static void DisplayTasks(List<Task> tasks)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Seznam úkolů:");
            Console.ResetColor();
            if (!tasks.Any())
            {
                Console.WriteLine("Žádné úkoly k zobrazení.");
                return;
            }

            var sortedTasks = tasks.OrderBy(t => t.Priority).ThenBy(t => t.DueDate);
            foreach (var task in sortedTasks)
            {
                Console.WriteLine(task);
            }
        }

        static void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nNabídka:");
            Console.WriteLine("1. Přidat úkol");
            Console.WriteLine("2. Vymazat úkol");
            Console.WriteLine("3. Označit úkol jako splněný");
            Console.WriteLine("4. Upravit úkol");
            Console.WriteLine("5. Vymazat všechny splněné úkoly");
            Console.WriteLine("6. Vymazat všechny úkoly");
            Console.WriteLine("7. Ukončit aplikaci");
            Console.ResetColor();
        }

        static void AddTask(List<Task> tasks)
        {
            Console.Clear();
            Console.Write("Zadejte název úkolu: ");
            string name = Console.ReadLine();

            DateTime dueDate;
            while (true)
            {
                Console.Write("Zadejte datum dokončení (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out dueDate))
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Neplatný formát data. Zkuste to znovu.");
                Console.ResetColor();
            }

            Priority priority;
            while (true)
            {
                Console.Write("Zadejte prioritu (1 = Vysoká, 2 = Střední, 3 = Nízká): ");
                if (int.TryParse(Console.ReadLine(), out int p) && Enum.IsDefined(typeof(Priority), p))
                {
                    priority = (Priority)p;
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Neplatná priorita. Zadejte číslo mezi 1 a 3.");
                Console.ResetColor();
            }

            int id = tasks.Count > 0 ? tasks.Max(t => t.ID) + 1 : 1;
            tasks.Add(new Task(id, name, dueDate, priority));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Úkol byl úspěšně přidán.");
            Console.ResetColor();
            Console.ReadKey();
        }

        static void DeleteTask(List<Task> tasks)
        {
            Console.Clear();
            Console.Write("Zadejte ID úkolu ke smazání: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var task = tasks.FirstOrDefault(t => t.ID == id);
                if (task != null)
                {
                    tasks.Remove(task);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Úkol byl úspěšně smazán.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Úkol s daným ID neexistuje.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Neplatné ID.");
                Console.ResetColor();
            }
            Console.ReadKey();
        }

        static void MarkTaskAsCompleted(List<Task> tasks)
        {
            Console.Clear();
            Console.Write("Zadejte ID úkolu k označení jako splněného: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var task = tasks.FirstOrDefault(t => t.ID == id);
                if (task != null)
                {
                    task.IsCompleted = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Úkol byl označen jako splněný.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Úkol s daným ID neexistuje.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Neplatné ID.");
                Console.ResetColor();
            }
            Console.ReadKey();
        }

        static void DeleteCompletedTasks(List<Task> tasks)
        {
            tasks.RemoveAll(t => t.IsCompleted);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Všechny splněné úkoly byly smazány.");
            Console.ResetColor();
            Console.ReadKey();
        }

        static void DeleteAllTasks(List<Task> tasks)
        {
            Console.Write("Opravdu chcete smazat všechny úkoly? (A/N): ");
            if (Console.ReadKey().Key == ConsoleKey.A)
            {
                tasks.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nVšechny úkoly byly smazány.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAkce byla zrušena.");
                Console.ResetColor();
            }
            Console.ReadKey();
        }
    }
}
        