using System.ComponentModel.DataAnnotations;

namespace SkoBingo.Models
{
    public class Sentence
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text { get; set; }

        public int BingoId { get; set; }
        public Bingo Bingo { get; set; }
    }
}