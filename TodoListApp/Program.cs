Console.WriteLine("Работу выполнили Семикин и Слесарев");

Console.WriteLine("Введите имя:");
string firstName = Console.ReadLine() ?? "";

Console.WriteLine("Введите фамилию:");
string lastName = Console.ReadLine() ?? "";

Console.WriteLine("Введите год рождения:");
string birthYearText = Console.ReadLine() ?? "";

int birthYear = int.Parse(birthYearText);
int age = DateTime.Now.Year - birthYear;

Console.WriteLine($"Добавлен пользователь {firstName} {lastName}, возраст - {age}");