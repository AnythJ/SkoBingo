using System;

namespace SkoBingo.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime WinDate { get; set; }

        public int ScoreboardId { get; set; }
        public Scoreboard Scoreboard { get; set; }
    }
}
