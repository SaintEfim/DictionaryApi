using Dictionary.Domain.Entity;
using Dictionary.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Dictionary.Persistence.Repository
{
    public class GermanRussianDictionaryRepository : IGermanRussianDictionaryRepository
    {
        private readonly GermanRussianDictionaryDbContext _context;
        public GermanRussianDictionaryRepository(GermanRussianDictionaryDbContext context)
        {
            _context = context;
        }

        public Task<List<GermanRussianDictionary>> GetDictionariesAsync() => _context.Dictionaries.ToListAsync();

        public async Task<GermanRussianDictionary> GetDictionaryAsync(int dictionaryId) => 
            await _context.Dictionaries.FindAsync(new object[] { dictionaryId });

        public async Task InsertDictionaryAsync(GermanRussianDictionary dictionary) => 
            await _context.Dictionaries.AddAsync(dictionary);

        public async Task UpdateDictionaryAsync(GermanRussianDictionary dictionary)
        {
            var existingDictionary = await _context.Dictionaries.FindAsync(new object[] { dictionary.Id });
            if (existingDictionary == null) return;
            existingDictionary.RussianWord = dictionary.RussianWord;
            existingDictionary.RussianTranslation = dictionary.RussianTranslation;
            existingDictionary.GermanTranslation = dictionary.GermanTranslation;
            existingDictionary.Comments = dictionary.Comments;
        }

        public async Task DeleteDictionaryAsync(int dictionaryEntryId)
        {
            var existingDictionaryEntry = await _context.Dictionaries.FindAsync(new object[] { dictionaryEntryId });
            if (existingDictionaryEntry == null) return;
            _context.Dictionaries.Remove(existingDictionaryEntry);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
