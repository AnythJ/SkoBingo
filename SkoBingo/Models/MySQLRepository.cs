using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SkoBingo.Models
{
    public class MySQLRepository : IBingoRepository
    {
        private readonly AppDbContext context;

        public MySQLRepository(AppDbContext _context)
        {
            this.context = _context;
        }


        public async Task<Bingo> AddBingoAsync(Bingo bingo)
        {
            string uniqueLink = LinkGenerator.GetUniqueLink(10);

            while (await ContainsLinkAsync(uniqueLink))
            {
                uniqueLink = LinkGenerator.GetUniqueLink(10);
            }

            
            bingo.UniqueLink = uniqueLink;
            await context.AddAsync(bingo);
            await context.SaveChangesAsync();

            return bingo;
        }

        /// <summary>
        /// Determines whether database contains bingo with this 
        /// </summary>
        /// <param name="uniqueLink"></param>
        public async Task<bool> ContainsLinkAsync(string uniqueLink)
        {
            return await context.Bingos.AnyAsync(e => e.UniqueLink == uniqueLink);
        }

        public async Task<Bingo> GetBingoAsync(string uniqueLink)
        {
            Bingo bingo = await context.Bingos.FirstOrDefaultAsync(e => e.UniqueLink == uniqueLink);
            if (bingo == null) return null;

            bingo.Sentences = await context.Sentences.Where(e => e.BingoId == bingo.BingoId).ToListAsync();

            bingo.Scoreboard = await context.Scoreboards.FirstOrDefaultAsync(e => e.BingoId == bingo.BingoId);
            
            return bingo;
        }

        public async Task<Player> AddPlayerAsync(Player player)
        {
            await context.Players.AddAsync(player);
            await context.SaveChangesAsync();

            return player;
        }

        /// <summary>
        /// Gets all players from a bingo related to the <paramref name="scoreboardId"/>
        /// </summary>
        /// <param name="scoreboardId"></param>
        /// <returns><typeparamref name="IColection"></typeparamref> of players</returns>
        public ICollection<Player> GetPlayers(int scoreboardId)
        {
            return context.Players.Where(e => e.ScoreboardId == scoreboardId).ToList();
        }

    }
}
