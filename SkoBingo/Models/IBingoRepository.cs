using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkoBingo.Models
{
    public interface IBingoRepository
    {
        Task<Bingo> AddBingoAsync(Bingo bingo);
        Task<Bingo> GetBingoAsync(string uniqueLink);
        Task<bool> ContainsLinkAsync(string uniqueLink);
        Task<Player> AddPlayerAsync(Player player);
        ICollection<Player> GetPlayers(int scoreboardId);
    }
}
