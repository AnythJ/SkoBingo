using SkoBingo.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SkoBingo.ViewModels
{
    public class CreationViewModel
    {
        public int Id { get; set; }
        public string UniqueLinkName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }
        [Required]
        public int Size { get; set; }
        public List<Sentence> Sentences { get; set; }
    }
}
