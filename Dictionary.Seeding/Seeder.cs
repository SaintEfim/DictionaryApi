using Dictionary.Domain.Entity;
using Dictionary.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Dictionary.Seeding
{
    public class Seeder
    {
        private readonly GermanRussianDictionaryDbContext _context;
        public Seeder(GermanRussianDictionaryDbContext context) 
        {
            _context = context;
        }

        public async Task SeedData()
        {
            try
            {
                // Путь к вашему JSON-файлу с данными
                string jsonFilePath = "C:\\Users\\user\\source\\Dictionary\\Dictionary.Seeding\\DataBase\\dictionary.json";
                Console.WriteLine(jsonFilePath);
                // Проверяем, существует ли файл
                if (File.Exists(jsonFilePath))
                {
                    // Считываем данные из JSON-файла
                    string jsonData = await File.ReadAllTextAsync(jsonFilePath);

                    // Десериализуем JSON в объекты вашей модели данных
                    var data = JsonSerializer.Deserialize<GermanRussianDictionary[]>(jsonData);

                    // Добавляем данные в контекст базы данных и сохраняем их
                    _context.AddRange(data);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("Файл с данными не найден.");
                }
            }
            catch (DbUpdateException ex)
            {
                // Печать внутреннего исключения для получения подробностей
                Console.WriteLine($"Ошибка сохранения данных: {ex.InnerException}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при импорте данных: {ex.Message}");
            }
        }
    }
}
