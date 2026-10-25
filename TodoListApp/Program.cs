﻿[25.10.2026 13:47] Виталити: using System;

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
                Console.WriteLine("Ошибка: Укажите текст задачи в кавычках.");
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
[25.10.2026 13:47] Виталити: Console.WriteLine("Список задач:");
            for (int i = 0; i < count; i++)
            {
                string statusText = statuses[i] ? "выполнено" : "не выполнено";
                Console.WriteLine($"{i + 1}. {todos[i]} — [{statusText}] (дата: {dates[i]:dd.MM.yyyy HH:mm:ss})");
            }
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