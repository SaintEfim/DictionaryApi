using System.ComponentModel.DataAnnotations;

namespace Dictionary.Api.Models
{
    public class CreateGermanRussianDictionaryDto
    {
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
    }
}
