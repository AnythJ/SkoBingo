using SkoBingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SkoBingo
{
    
    public static class Helper
    {
        public static void BasicShuffle(Bingo bingo)
        {
            int n = bingo.Sentence.Count;
            var list = bingo.Sentence.ToList();

            while(n > 1)
            {
                Random r = new Random();
                n--;
                int k = r.Next(0, bingo.Sentence.Count);
                Sentence value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            bingo.Sentence = list;
        }
    }
}
