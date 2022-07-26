using Microsoft.EntityFrameworkCore;
using SkoBingo.Models;

namespace SkoBingo.Tests
{
    public class MySQLRepostioryTests
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;
        public MySQLRepostioryTests()
        {
            var serverVersion = new MySqlServerVersion(new Version(5, 6, 50));
            string connStr = "Server=127.0.0.1;Port=3306;Database=skobingotestdb;Uid=root;Pwd=admin;SslMode=None;AllowPublicKeyRetrieval=True";

            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connStr, serverVersion)
                .Options;
        }

        private async Task<MySQLRepository> CreateRepositoryAsync()
        {
            AppDbContext context = new AppDbContext(dbContextOptions);
            await GenerateDataAsync(context);
            return new MySQLRepository(context);
        }

        public Bingo CreateTestBingo(int number, int size)
        {
            Bingo bingo = new Bingo()
            {
                Name = "BingoNumber" + number,
                Sentences = new List<Sentence>(),
                Size = size
            };

            Scoreboard scoreboard = new Scoreboard();

            for (int i = 1; i <= size * size; i++)
            {
                bingo.Sentences.Add(new Sentence()
                {
                    Text = "BingoNumber" + number + "Text" + i
                });
            }

            return bingo;
        }

        public Player CreateTestPlayer(int number, int scoreboardId)
        {
            Player player = new Player()
            {
                Name = "Player" + number,
                ScoreboardId = scoreboardId,
                WinDate = DateTime.Now
            };

            return player;
        }

        private async Task GenerateDataAsync(AppDbContext context)
        {
            for (int i = 1; i <= 3; i++)
            {
                Bingo bingo = CreateTestBingo(i, i);

                string uniqueLink = LinkGenerator.GetUniqueLink(10);

                while (await ContainsLinkAsync(uniqueLink, context))
                {
                    uniqueLink = LinkGenerator.GetUniqueLink(10);
                }

                bingo.UniqueLink = uniqueLink;

                await context.AddAsync(bingo);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> ContainsLinkAsync(string uniqueLink, AppDbContext context)
        {
            return await context.Bingos.AnyAsync(e => e.UniqueLink == uniqueLink);
        }


        [Theory]
        [InlineData(4, 1)]
        [InlineData(5, 5)]
        [InlineData(6, 10)]
        public async Task AddBingoAsyncAsync_SingleObject_AfterAdditionDBContextShouldContainTheSameObject(int number, int size)
        {
            // Arrange
            Bingo bingo = CreateTestBingo(number, size);
            var repository = await CreateRepositoryAsync();

            // Act
            Bingo addedBingo = await repository.AddBingoAsync(bingo);
            Bingo bingoFromDbContext = await repository.GetBingoAsync(addedBingo.UniqueLink);

            // Assert
            Assert.Equal(addedBingo.UniqueLink, bingoFromDbContext.UniqueLink);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public async Task AddPlayerAsyncAsync_SingleObject_AddedPlayerShouldBeReturnedFromGetPlayers(int number, int scoardboardId)
        {
            // Arrange
            Player player = CreateTestPlayer(number, scoardboardId);
            var repository = await CreateRepositoryAsync();

            // Act
            Player addedPlayer = await repository.AddPlayerAsync(player);
            List<Player> listOfPlayersFromDbContext = repository.GetPlayers(scoardboardId).ToList();

            // Assert
            Assert.Contains(addedPlayer, listOfPlayersFromDbContext);
        }
    }
}