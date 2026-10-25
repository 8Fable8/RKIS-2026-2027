﻿[25.10.2026 13:47] Виталити: using System;

using System;
using System.Text;

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
                        ViewTasks(todos, statuses, dates, todoCount, argument);
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
            Console.WriteLine("  help                               - список команд");
            Console.WriteLine("  profile                            - данные пользователя");
            Console.WriteLine("  add [--multiline | -m]             - добавить задачу (обычная или многострочная)");
            Console.WriteLine("  view [флаги]                       - просмотр (флаги: -i, -s, -d, -a или --index, --status, --update-date, --all)");
            Console.WriteLine("  exit                               - выход");
        }

        private static void PrintProfile(string firstName, string lastName, int birthYear)
        {
            Console.WriteLine($"Профиль: {firstName} {lastName}, {birthYear}");
        }

        private static void AddTask(ref string[] todos, ref bool[] statuses, ref DateTime[] dates, ref int count, string argument)
        {
            bool isMultiline = argument.Contains("--multiline") || argument.Contains("-m");
            string taskText = string.Empty;

            if (isMultiline)
            {
                Console.WriteLine("Многострочный режим. Вводите строки. Для завершения введите '!end':");
                StringBuilder sb = new StringBuilder();
                bool firstLine = true;

                while (true)
                {
                    Console.Write("> ");
                    string line = Console.ReadLine();
                    if (line == null || line.Trim() == "!end") break;

                    if (!firstLine)
                    {
                        sb.Append("\n");
                    }
                    sb.Append(line);
                    firstLine = false;
                }
                taskText = sb.ToString();
            }
            else
            {
                Console.Write("Введите текст задачи: ");
                string line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine("Ошибка: Текст задачи не может быть пустым.");
                    return;
                }
                taskText = line.Trim('"');
            }

            if (string.IsNullOrEmpty(taskText))
            {
                Console.WriteLine("Ошибка: Задача пуста, не добавлена.");
                return;
            }

            if (count >= todos.Length)
            {
                ExpandArrays(ref todos, ref statuses, ref dates);
            }

            todos[count] = taskText;
            statuses[count] = false;
            dates[count] = DateTime.Now;
            count++;

            Console.WriteLine("Задача успешно добавлена!");
        }

        private static void ViewTasks(string[] todos, bool[] statuses, DateTime[] dates, int count, string argument)
        {
            if (count == 0)
            {
                Console.WriteLine("Список задач пуст.");
                return;
            }

            bool showIndex = argument.Contains("--index") || argument.Contains("-i") || argument.Contains("--all") || argument.Contains("-a");
            bool showStatus = argument.Contains("--status") || argument.Contains("-s") || argument.Contains("--all") || argument.Contains("-a");
            bool showDate = argument.Contains("--update-date") || argument.Contains("-d") || argument.Contains("--all") || argument.Contains("-a");

            Console.WriteLine("\n--- Список задач ---");
            for (int i = 0; i < count; i++)
            {
                if (todos[i] == null) continue;

                string line = "";
                if (showIndex)
                {
                    line += $"[{i + 1}] ";
                }

                // Обрезка текста до 30 символов, если длиннее — добавляем ...
                string rawText = todos[i].Replace("\n", " "); // для табличного вида заменяем переносы на пробелы
                string truncatedText = rawText.Length > 30 ? rawText.Substring(0, 27) + "..." : rawText;
                line += $"Задача: {truncatedText,-33}";

                if (showStatus)
                {
                    string statusText = statuses[i] ? "Выполнено" : "Не выполнено";
                    line += $" | Статус: {statusText,-13}";
                }

                if (showDate)
                {
                    line += $" | Дата: {dates[i]:dd.MM.yyyy HH:mm}";
                }

                Console.WriteLine(line);
            }
            Console.WriteLine("--------------------");
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