using System.Collections.Generic;

namespace SkoBingo.Models
{
    public class Scoreboard
    {
        public int ScoreboardId { get; set; }

        public ICollection<Player> Players { get; set; }

        public int BingoId { get; set; }
        public Bingo Bingo { get; set; }
    }
}
