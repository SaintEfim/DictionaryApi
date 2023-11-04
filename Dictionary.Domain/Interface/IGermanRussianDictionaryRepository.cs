using Dictionary.Domain.Entity;

namespace Dictionary.Domain.Interface
{
    public interface IGermanRussianDictionaryRepository
    {
        Task<List<GermanRussianDictionary>> GetDictionariesAsync();
        Task<GermanRussianDictionary> GetDictionaryAsync(int dictionaryId);
        Task InsertDictionaryAsync(GermanRussianDictionary dictionary);
        Task UpdateDictionaryAsync(GermanRussianDictionary dictionary);
        Task DeleteDictionaryAsync(int dictionaryId);
        Task SaveAsync();
    }
}
