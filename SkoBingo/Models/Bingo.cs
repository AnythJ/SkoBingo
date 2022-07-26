using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkoBingo.Models
{
    public class Bingo
    {
        public int BingoId { get; set; }

        [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Size is required")]
        [Range(1, 10, ErrorMessage = "Must be between 1 and 10")]
        public int Size { get; set; }

        [Required(ErrorMessage = "Link is required", AllowEmptyStrings = false)]
        public string UniqueLink { get; set; }

        [Required(ErrorMessage = "All sentences are required", AllowEmptyStrings = false)]
        public ICollection<Sentence> Sentences { get; set; }

        public Scoreboard Scoreboard { get; set; }
    }
}