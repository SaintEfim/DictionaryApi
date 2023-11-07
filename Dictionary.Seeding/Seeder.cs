using Dictionary.Domain.Entity;
using Dictionary.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.IO;
using System;
using System.Threading.Tasks;

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
                    if (!_context.Dictionaries.Any())
                    {
                        // Считываем данные из JSON-файла
                        string jsonData = await File.ReadAllTextAsync(jsonFilePath);

                        // Десериализуем JSON в объекты вашей модели данных
                        var data = JsonSerializer.Deserialize<GermanRussianDictionary[]>(jsonData);

                        // Путь к папке с фотографиями
                        string photoFolderPath = "C:\\Users\\user\\source\\Dictionary\\Photo";

                        // Получаем все файлы из папки
                        string[] photoFiles = Directory.GetFiles(photoFolderPath);

                        // Перебираем все файлы
                        for (int i = 0; i < photoFiles.Length && i < data.Length; i++)
                        {
                            if (data[i].Photo == null)
                            {
                                byte[] imageBytes = File.ReadAllBytes(photoFiles[i]);

                                // Запись изображения в поле Photo
                                data[i].Photo = imageBytes;
                            }
                        }

                        // Добавляем данные в контекст базы данных и сохраняем их
                        _context.AddRange(data);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        Console.WriteLine("База данных уже содержит данные. Записи не добавлены.");
                    }
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
