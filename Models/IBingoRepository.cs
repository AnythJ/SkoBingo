﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkoBingo.Models
{
    public interface IBingoRepository
    {
        List<string> GetLinks();
        Bingo Add(Bingo bingo);
        Bingo GetBingo(string uniqueLink);
        ICollection<Sentence> GetSentences();
        bool ContainsLink(string uniqueLink);

        public Player AddPlayer(Player player);

        ICollection<Player> GetPlayers(int scoreboardId);
    }
}
