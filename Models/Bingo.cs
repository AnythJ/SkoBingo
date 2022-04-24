using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkoBingo.Models
{
    public class Bingo
    {
        public int BingoId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        [Required]
        public int Size { get; set; }
        public string UniqueLink { get; set; }

        public ICollection<Question> Question { get; set; }
    }
}