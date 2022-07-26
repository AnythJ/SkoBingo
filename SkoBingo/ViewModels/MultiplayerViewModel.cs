using System.ComponentModel.DataAnnotations;

namespace SkoBingo.ViewModels
{
    public class MultiplayerViewModel
    {
        [Required(ErrorMessage = "Link is required", AllowEmptyStrings = false)]
        public string UniqueLink { get; set; }
    }
}
