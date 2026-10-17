﻿using System;

namespace TodoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Работу выполнили: Слесарев и Семикин");
            Console.WriteLine(new string('-', 40));

            
            Console.Write("Введите ваше имя: ");
            string firstName = Console.ReadLine();

            Console.Write("Введите вашу фамилию: ");
            string lastName = Console.ReadLine();

            Console.Write("Введите ваш год рождения: ");
            string birthYearInput = Console.ReadLine();

            
            if (int.TryParse(birthYearInput, out int birthYear))
            {
                int currentYear = DateTime.Now.Year;
                int age = currentYear - birthYear;

                
                Console.WriteLine($"Добавлен пользователь {firstName} {lastName}, возраст - {age}");
            }
            else
            {
                Console.WriteLine("Ошибка: вы ввели некорректный год рождения.");
                return;
            }

            // Лекция 3
            string[] todos = new string[2];
            int count = 0;

            while (true)
            {
                Console.Write("> ");
                string input = (Console.ReadLine() ?? "").Trim();
                string command = input.Split(' ')[0];

                if (command == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неизвестная команда. Введите help, чтобы увидеть список команд.");
                }
            }
        }
    }
}