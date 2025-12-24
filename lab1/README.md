# Laba1_Solodovnikova
Лаба 1 Солодовникова Милена К0709-23/3
# Лабораторная работа № 1 

## Тема
Работа с JSON, файлами и освобождением ресурсов

## Что было реализовано:
1. Класс Person с полями из задания
2. Сериализатор для работы с JSON
3. Менеджер файлов с IDisposable
4. Тестирование всего этого

## Часть 1: Класс Person

### Что есть в классе:
- **FirstName, LastName, Age** - обычные поля
- **Password** - не попадает в JSON (помечено [JsonIgnore])
- **Id** - в JSON называется "personId"
- **_birthDate** - приватное поле, доступ через BirthDate
- **Email** - проверяет есть ли @ в setter
- **PhoneNumber** - в JSON называется "phone"
- **FullName** - только чтение (имя+фамилия)
- **IsAdult** - только чтение (возраст >= 18)

### Особенности:
- Email кидает ошибку если нет @
- Password не видно в JSON файлах
- _birthDate приватный но сериализуется
- 

## Часть 2: PersonSerializer

### 8 методов:

1. **SerializeToJson()** - Person → строка JSON
2. **DeserializeFromJson()** - строка JSON → Person
3. **SaveToFile()** - сохраняет Person в файл (обычно)
4. **LoadFromFile()** - грузит Person из файла (обычно)
5. **SaveToFileAsync()** - то же но асинхронно
6. **LoadFromFileAsync()** - то же но асинхронно
7. **SaveListToFile()** - список Person → файл
8. **LoadListFromFile()** - файл → список Person

### Как работает:
- JSON красиво форматируется (с отступами)
- Используется UTF-8 кодировка
- Есть обработка ошибок


## Часть 3: FileResourceManager

### Поля (как в задании):
- `_fileStream` - для работы с файлом
- `_writer`, `_reader` - для текста
- `_disposed` - флаг что ресурсы освобождены
- `_filePath` - путь к файлу

### Что умеет:
- **OpenForWriting()** - открыть файл для записи
- **OpenForReading()** - открыть файл для чтения
- **WriteLine()** - записать строку
- **ReadAllText()** - прочитать весь файл
- **AppendText()** - добавить текст в конец
- **GetFileInfo()** - информация о файле (размер, дата)

### IDisposable:
- Реализован паттерн Dispose
- Есть финализатор на всякий случай
- Ресурсы освобождаются правильно


## Часть 4: Тестирование

### Проверка:
1. Создание Person с правильным Email
2. Сериализация в JSON и обратно
3. Сохранение в файл и загрузка
4. Работа со списками
5. FileResourceManager освобождает ресурсы

## Что создаст программа:
person.json - объект Person в JSON
people.json - список Person
log.txt - тестовый файл
errors.log - ошибки если будут
