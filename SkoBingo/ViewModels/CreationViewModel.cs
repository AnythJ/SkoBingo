using SkoBingo.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SkoBingo.ViewModels
{
    public class CreationViewModel
    {
        public int BingoId { get; set; }

        [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Size is required")]
        [Range(1, 10, ErrorMessage = "Must be between 1 and 10")]
        public int Size { get; set; }
        public string UniqueLink { get; set; }

        [Required(ErrorMessage = "All sentences are required", AllowEmptyStrings = false)]
        public List<Sentence> Sentences { get; set; }

        public Scoreboard Scoreboard { get; set; }
    }
}
