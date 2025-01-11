using MongoDB.Bson;
using MongoDB.Driver;
using PokemonAPIApplication.Models;

namespace PokemonAPIApplication.Services
{
    public class PokemonService : IPokemonService
    {

        private readonly IMongoCollection<Pokemon> _pokemonsCollection;

        public PokemonService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
            _pokemonsCollection = database.GetCollection<Pokemon>(configuration["MongoDbSettings:CollectionName"]);
        }

        public async Task<List<Pokemon>> GetPokemons()
        {
            return await _pokemonsCollection.Find(pokemon => true).ToListAsync();
        }

        public async Task<Pokemon> GetPokemonById(string id)
        {
            var pokemon = await _pokemonsCollection.Find(pokemon => pokemon.Id == id).FirstOrDefaultAsync();
            return pokemon ?? throw new Exception("Pokemon not found");
        }

        public async Task<List<Pokemon>> GetPokemonByName(string name)
        {
            var pokemon = await _pokemonsCollection.Find(pokemon => pokemon.Name == name).ToListAsync();
            return pokemon ?? throw new Exception("Pokemon not found");
        }

        public async Task<Pokemon> AddPokemon(Pokemon newPokemon)
        {
            if (string.IsNullOrEmpty(newPokemon.Id))
            {
                newPokemon.Id = ObjectId.GenerateNewId().ToString();
            }
            await _pokemonsCollection.InsertOneAsync(newPokemon);
            return newPokemon;
        }

        public async Task<Pokemon> UpdatePokemon(string id, Pokemon updatedPokemon)
        {
            var result = await _pokemonsCollection.ReplaceOneAsync(pokemon => pokemon.Id == id, updatedPokemon);
            if (result.MatchedCount == 0)
            {
                throw new Exception("Pokemon not found");
            }
            return updatedPokemon;
        }

        public async Task<bool> DeletePokemon(string id)
        {
            var result = await _pokemonsCollection.DeleteOneAsync(pokemon => pokemon.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new Exception("Pokemon not found");
            }
            return true;
        }
    }
}
