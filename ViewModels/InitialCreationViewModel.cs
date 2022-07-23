using System.ComponentModel.DataAnnotations;

namespace SkoBingo.ViewModels
{
    public class InitialCreationViewModel
    {
        [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Size is required")]
        [Range(1, 10, ErrorMessage = "Must be between 1 and 10")]
        public int Size { get; set; }
    }
}
