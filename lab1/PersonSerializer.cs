using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Lab1
{
    public class PersonSerializer
    {
        public string SerializeToJson(Person person)
        {
            var opt = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(person, opt);
        }
        
        public Person DeserializeFromJson(string json)
        {
            var person = JsonSerializer.Deserialize<Person>(json);
            if (person == null)
                throw new Exception("Не получилось :((");
            return person;
        }

        public void SaveToFile(Person person, string filePath)
        {
            try
            {
                string json = SerializeToJson(person);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                LogError("SaveToFile", ex.Message);
                throw;
            }
        }

        public Person LoadFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("Файл не найден", filePath);
                    
                string json = File.ReadAllText(filePath);
                return DeserializeFromJson(json);
            }
            catch (Exception ex)
            {
                LogError("LoadFromFile", ex.Message);
                throw;
            }
        }

        public async Task SaveToFileAsync(Person person, string filePath)
        {
            try
            {
                string json = SerializeToJson(person);
                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                LogError("SaveToFileAsync", ex.Message);
                throw;
            }
        }

        public async Task<Person> LoadFromFileAsync(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("Файл не найден", filePath);
                    
                string json = await File.ReadAllTextAsync(filePath);
                return DeserializeFromJson(json);
            }
            catch (Exception ex)
            {
                LogError("LoadFromFileAsync", ex.Message);
                throw;
            }
        }

        public void SaveListToFile(List<Person> people, string filePath)
        {
            try
            {
                var opt = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(people, opt);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                LogError("SaveListToFile", ex.Message);
                throw;
            }
        }
        
        public List<Person> LoadListFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return new List<Person>();
                    
                string json = File.ReadAllText(filePath);
                var list = JsonSerializer.Deserialize<List<Person>>(json);
                return list ?? new List<Person>();
            }
            catch (Exception ex)
            {
                LogError("LoadListFromFile", ex.Message);
                return new List<Person>();
            }
        }

        private void LogError(string method, string error)
        {
            string log = $"{DateTime.Now}: {method} - {error}";
            File.AppendAllText("errors.log", log + Environment.NewLine);
        }
    }
}