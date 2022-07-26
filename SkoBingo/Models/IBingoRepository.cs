using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkoBingo.Models
{
    public interface IBingoRepository
    {
        Task<Bingo> AddBingo(Bingo bingo);
        Task<Bingo> GetBingo(string uniqueLink);
        Task<bool> ContainsLink(string uniqueLink);
        Task<Player> AddPlayer(Player player);
        ICollection<Player> GetPlayers(int scoreboardId);
    }
}
