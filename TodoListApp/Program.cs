﻿[25.10.2026 13:47] Виталити: using System;

using System;

namespace TodoListApp
{
    class Program
    {
        private const int InitialCapacity = 2;
        private const string AppName = "TodoListApp";

        static void Main(string[] args)
        {
            string firstName = "Виталий";
            string lastName = "Слесарев";
            int birthYear = 2004;

            string[] todos = new string[InitialCapacity];
            bool[] statuses = new bool[InitialCapacity];
            DateTime[] dates = new DateTime[InitialCapacity];
            int todoCount = 0;

            Console.WriteLine($"{AppName} запущен. Введите 'help' для списка команд.");

            bool isRunning = true;
            while (isRunning)
            {
                Console.Write("\nВведите команду: ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input)) continue;

                string[] parts = input.Split(new[] { ' ' }, 2);
                string command = parts[0].ToLower();
                string argument = parts.Length > 1 ? parts[1] : string.Empty;

                switch (command)
                {
                    case "help":
                        PrintHelp();
                        break;
                    case "profile":
                        PrintProfile(firstName, lastName, birthYear);
                        break;
                    case "add":
                        AddTask(ref todos, ref statuses, ref dates, ref todoCount, argument);
                        break;
                    case "view":
                        ViewTasks(todos, statuses, dates, todoCount);
                        break;
                    case "done":
                        MarkTaskDone(statuses, dates, todoCount, argument);
                        break;
                    case "update":
                        UpdateTask(todos, dates, todoCount, argument);
                        break;
                    case "delete":
                        DeleteTask(ref todos, ref statuses, ref dates, ref todoCount, argument);
                        break;
                    case "exit":
                        isRunning = false;
                        Console.WriteLine("Завершение работы программы...");
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда. Введите 'help'.");
                        break;
                }
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Доступные команды:");
            Console.WriteLine("  help                   - список команд");
            Console.WriteLine("  profile                - данные пользователя");
            Console.WriteLine("  add \"текст\"            - добавить задачу");
            Console.WriteLine("  view                   - посмотреть задачи");
            Console.WriteLine("  done <индекс>          - отметить выполненной");
            Console.WriteLine("  update <индекс> \"текст\"- обновить текст задачи");
            Console.WriteLine("  delete <индекс>        - удалить задачу");
            Console.WriteLine("  exit                   - выход");
        }

        private static void PrintProfile(string firstName, string lastName, int birthYear)
        {
            Console.WriteLine($"Профиль: {firstName} {lastName}, {birthYear}");
        }

        private static void AddTask(ref string[] todos, ref bool[] statuses, ref DateTime[] dates, ref int count, string argument)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                Console.WriteLine("Ошибка: Укажите текст задачи.");
                return;
            }

            if (count >= todos.Length)
            {
                ExpandArrays(ref todos, ref statuses, ref dates);
            }

            todos[count] = argument.Trim('"');
            statuses[count] = false;
            dates[count] = DateTime.Now;
            count++;

            Console.WriteLine("Задача успешно добавлена!");
        }

        private static void ViewTasks(string[] todos, bool[] statuses, DateTime[] dates, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Список задач пуст.");
                return;
            }

            Console.WriteLine("Список задач:");
            for (int i = 0; i < count; i++)
            {
                string statusText = statuses[i] ? "выполнено" : "не выполнено";
                Console.WriteLine($"{i + 1}. {todos[i]} — [{statusText}] (дата: {dates[i]:dd.MM.yyyy HH:mm:ss})");
            }
        }

        private static void MarkTaskDone(bool[] statuses, DateTime[] dates, int count, string argument)
        {
            if (!int.TryParse(argument, out int index) || index < 1 || index > count)
            {
                Console.WriteLine("Ошибка: Неверный номер задачи.");
                return;
            }

            int realIndex = index - 1;
            statuses[realIndex] = true;
            dates[realIndex] = DateTime.Now;
            Console.WriteLine($"Задача {index} отмечена как выполненная!");
        }

        private static void UpdateTask(string[] todos, DateTime[] dates, int count, string argument)
        {
            string[] parts = argument.Split(new[] { ' ' }, 2);
            if (parts.Length < 2 || !int.TryParse(parts[0], out int index) || index < 1 || index > count)
            {
                Console.WriteLine("Ошибка формат. Пример: update 1 \"Новый текст\"");
                return;
            }

            int realIndex = index - 1;
            todos[realIndex] = parts[1].Trim('"');
            dates[realIndex] = DateTime.Now;
            Console.WriteLine($"Задача {index} успешно обновлена!");
        }

        private static void DeleteTask(ref string[] todos, ref bool[] statuses, ref DateTime[] dates, ref int count, string argument)
        {
            if (!int.TryParse(argument, out int index) || index < 1 || index > count)
            {
                Console.WriteLine("Ошибка: Неверный номер задачи.");
                return;
            }

            int realIndex = index - 1;

            // Сдвиг элементов влево
            for (int i = realIndex; i < count - 1; i++)
            {
                todos[i] = todos[i + 1];
                statuses[i] = statuses[i + 1];
                dates[i] = dates[i + 1];
            }

            todos[count - 1] = null;
            statuses[count - 1] = false;
            dates[count - 1] = default;
            count--;

            Console.WriteLine($"Задача {index} удалена!");
        }

        private static void ExpandArrays(ref string[] todos, ref bool[] statuses, ref DateTime[] dates)
        {
            int newSize = todos.Length * 2;
            string[] newTodos = new string[newSize];
            bool[] newStatuses = new bool[newSize];
            DateTime[] newDates = new DateTime[newSize];

            for (int i = 0; i < todos.Length; i++)
            {
                newTodos[i] = todos[i];
                newStatuses[i] = statuses[i];
                newDates[i] = dates[i];
            }

            todos = newTodos;
            statuses = newStatuses;
            dates = newDates;
        }
    }
}