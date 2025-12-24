using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine(" Лаба 1. Сериализация и файлы\n");

            // 1. Создаём персону
            var person = new Person("Неиван", "непетров", 99, "tantum_verde@gmail.com");
            person.Id = "001";
            person.PhoneNumber = "+78005553535";
            person.BirthDate = new DateTime(1927, 5, 10);

            Console.WriteLine($"Создали: {person.FullName}");
            Console.WriteLine($"Взрослый: {person.IsAdult}");
            Console.WriteLine($"Email: {person.Email}");

            // 2. Работаем с сериализатором
            var serializer = new PersonSerializer();

            // В строку
            string json = serializer.SerializeToJson(person);
            Console.WriteLine("\nJSON строка:");
            Console.WriteLine(json);

            // В файл и обратно
            serializer.SaveToFile(person, "person.json");
            var fromFile = serializer.LoadFromFile("person.json");
            Console.WriteLine($"\nЗагрузили из файла: {fromFile.FullName}");

            // Асинхронно
            await serializer.SaveToFileAsync(person, "person_async.json");
            var fromAsync = await serializer.LoadFromFileAsync("person_async.json");
            Console.WriteLine($"Асинхронно: {fromAsync.FullName}");

            // Список
            var people = new List<Person>
            {
                person,
                new Person("Михаил", "Петрович", 17, "kchao@mail.ru"),
                new Person("Алексей", "Чета", 33, "x@mail.ru")
            };

            serializer.SaveListToFile(people, "people.json");
            var loadedList = serializer.LoadListFromFile("people.json");
            Console.WriteLine($"\nЗагрузили список из {loadedList.Count} человек");

            // 3. Проверяем FileResourceManager
            Console.WriteLine("\nТест FileManager");

            using (var manager = new FileResourceManager("test.txt"))
            {
                manager.OpenForWriting();
                manager.WriteLine("Первая строка");
                manager.WriteLine("Вторая строка");
                manager.AppendText("Строка");
            }

            using (var manager = new FileResourceManager("test.txt"))
            {
                manager.OpenForReading();
                string content = manager.ReadAllText();
                Console.WriteLine("Содержимое файла:");
                Console.WriteLine(content);

                var info = manager.GetFileInfo();
                Console.WriteLine($"Размер файла: {info.Length} байт");
                Console.WriteLine($"Создан: {info.CreationTime}");
            }

            try
            {
                var badPerson = new Person();
                badPerson.Email = "неправильный email";
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nАшибка валидации: {ex.Message}");
            }

            Console.WriteLine("\nНет ошибка");
        }
    }
}
