using System;

namespace TodoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Шаг 7: Измените текст, который выводится в консоль (укажите вашу фамилию и напарника)
            Console.WriteLine("Работу выполнили: Слесарев и  Семикин");
            Console.WriteLine(new string('-', 40));

            // Шаг 8: Написание программы
            // 1. Попросить пользователя ввести имя, фамилию и год рождения
            Console.Write("Введите ваше имя: ");
            string firstName = Console.ReadLine();

            Console.Write("Введите вашу фамилию: ");
            string lastName = Console.ReadLine();

            Console.Write("Введите ваш год рождения: ");
            string birthYearInput = Console.ReadLine();

            // 2 & 3. Переводим год рождения в целое число и вычисляем возраст
            if (int.TryParse(birthYearInput, out int birthYear))
            {
                int currentYear = DateTime.Now.Year;
                int age = currentYear - birthYear;

                // 4. Выводим сообщение заданного формата
                Console.WriteLine($"Добавлен пользователь {firstName} {lastName}, возраст - {age}");
            }
            else
            {
                Console.WriteLine("Ошибка: вы ввели некорректный год рождения.");
            }
        }
    }
}
