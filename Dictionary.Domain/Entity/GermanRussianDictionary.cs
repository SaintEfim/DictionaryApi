using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Domain.Entity
{
    public class GermanRussianDictionary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string RussianWord { get; set; }

        [Required]
        public string RussianTranslation { get; set; }

        [Required]
        public string GermanTranslation { get; set; }
        public string? Comments { get; set; }

        private DateTime _entryDate;

        [Required]
        public DateTime EntryDate
        {
            get => _entryDate;
            set => _entryDate = value.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
                : value.ToUniversalTime();
        }
        public byte[]? Photo { get; set; }
    }
}
