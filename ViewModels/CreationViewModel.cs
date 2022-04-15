using SkoBingo.Models;
using System.Collections.Generic;

namespace SkoBingo.ViewModels
{
    public class CreationViewModel
    {
        public int Id { get; set; }
        public string UniqueLinkName { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public List<Question> Questions { get; set; }
    }
}
