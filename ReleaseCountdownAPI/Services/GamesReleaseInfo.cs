using MongoDB.Driver;
using ReleaseCountdownAPI.Entities;
using ReleaseCountdownAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReleaseCountdownAPI.Services
{
    public class GamesReleaseInfo
    {
        private readonly IMongoCollection<Game> _games;

        public GamesReleaseInfo(IGamesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _games = database.GetCollection<Game>(settings.GamesCollectionName);
        }

        public async Task Post(ReleaseItemModel model)
        {
            var game = new Game
            {
                Name = model.Name,
                ReleaseDate = model.ReleaseDate
            };
            await _games.InsertOneAsync(game).ConfigureAwait(false);
        }

        public async Task Delete(string id)
        {
           await _games.DeleteOneAsync(g => g.Id == id).ConfigureAwait(false);
        }

        public async Task<List<Game>> Get() => (await _games.FindAsync(x => true).ConfigureAwait(false)).ToList();

        public async Task<Game> Find(string gameName) => (await _games.FindAsync(g => g.Name == gameName)).First();

    }
}
