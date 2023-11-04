using Dictionary.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Persistence
{
    public class GermanRussianDictionaryDbContext : DbContext
    {
        public GermanRussianDictionaryDbContext(DbContextOptions<GermanRussianDictionaryDbContext> options) : base(options)
        {
            Database.Migrate();
        }
        public DbSet<GermanRussianDictionary> Dictionaries => Set<GermanRussianDictionary>();


    }
}
