using System;
using System.Data;

namespace ToDoConsoleApp
{
    class Program
    {
        static void Main()
        {
            ToDoService service = new ToDoService();

            while (true)
            {

                Console.WriteLine("\n=== To‑Do List Menu ===");
                Console.WriteLine("1. View all tasks");
                Console.WriteLine("2. Add new task");
                Console.WriteLine("3. Update task");
                Console.WriteLine("4. Delete task");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        service.ReadAll();
                        break;
                    case "2":
                        AddTask(service);
                        break;
                    case "3":
                        UpdateTask(service);
                        break;
                    case "4":
                        DeleteTask(service);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
                Console.WriteLine("Press any key to continue......");
                Console.ReadKey();
            }
        }

        

        static void AddTask(ToDoService service)
        {
            Console.Write("Title: "); var title = Console.ReadLine();
            Console.Write("Description (optional): "); var desc = Console.ReadLine();
            Console.Write("Due date (yyyy-MM-dd, leave blank for none): ");
            var dueInput = Console.ReadLine();
            DateTime? dueDate = DateTime.TryParse(dueInput, out var dt) ? dt : (DateTime?)null;

            service.Create(title, desc, dueDate);
        }

        static void UpdateTask(ToDoService service)
        {
            Console.Write("Task ID to update: "); if (!int.TryParse(Console.ReadLine(), out var id)) { Console.WriteLine("Invalid ID."); return; }
            Console.Write("New Title: "); var title = Console.ReadLine();
            Console.Write("New Description: "); var desc = Console.ReadLine();
            Console.Write("Is complete? (y/n): "); var isComp = Console.ReadLine().Trim().ToLower() == "y";
            Console.Write("Due date (yyyy-MM-dd, blank for none): ");
            DateTime? dueDate = DateTime.TryParse(Console.ReadLine(), out var dt) ? dt : (DateTime?)null;

            service.Update(id, title, desc, isComp, dueDate);
        }

        static void DeleteTask(ToDoService service)
        {
            Console.Write("Task ID to delete: "); if (!int.TryParse(Console.ReadLine(), out var id)) { Console.WriteLine("Invalid ID."); return; }
            service.Delete(id);
        }
    }
}
