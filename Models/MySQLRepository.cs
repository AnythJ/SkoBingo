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


        public Bingo Add(Bingo bingo)
        {
            string uniqueLink = LinkGenerator.GetUniqueLink(10);
            
            while(ContainsLink(uniqueLink))
            {
                uniqueLink = LinkGenerator.GetUniqueLink(10);
            }

            bingo.UniqueLink = uniqueLink;
            context.Add(bingo);
            context.SaveChanges();

            return bingo;
        }

        public bool ContainsLink(string uniqueLink)
        {
            return context.Bingos.Any(e => e.UniqueLink == uniqueLink);
        }

        public Bingo GetBingo(string uniqueLink)
        {
            Bingo bingo = context.Bingos.FirstOrDefault(e => e.UniqueLink == uniqueLink);
            bingo.Question = context.Questions.Where(e => e.BingoId == bingo.BingoId).ToList();

            return bingo;
        }

        public List<string> GetLinks()
        {
            List<string> test = new();
            test.Add("testLink");

            return test;
        }

        public ICollection<Question> GetQuestions()
        {
            throw new NotImplementedException();
        }
    }
}
