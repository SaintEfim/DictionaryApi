using Dictionary.Domain.Entity;
using Dictionary.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Reflection;

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
                var appDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\..\"));
                var fullPath = Path.Combine(Path.Combine(appDir, @"Dictionary.Seeding\DataBase\Dictionary.json"));

                // Проверяем, существует ли файл
                if (File.Exists(fullPath))
                {
                    if (!_context.Dictionaries.Any())
                    {
                        // Считываем данные из JSON-файла
                        string jsonData = await File.ReadAllTextAsync(fullPath);

                        // Десериализуем JSON в объекты вашей модели данных
                        var data = JsonSerializer.Deserialize<GermanRussianDictionary[]>(jsonData);

                        // Путь к папке с фотографиями
                        var photoFolderPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\..\"));
                        var fullphotoFolderPath = Path.Combine(Path.Combine(appDir, @"Photo"));

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
