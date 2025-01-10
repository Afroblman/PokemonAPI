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
        public List<Pokemon> GetPokemons()
        {
            return _pokemonsCollection.Find(pokemon => true).ToList();
        }

        public Pokemon GetPokemonById(string id)
        {
            var pokemon = _pokemonsCollection.Find(pokemon => pokemon.Id == id).FirstOrDefault();
            return pokemon == null ? throw new Exception("Pokemon not found") : pokemon;
        }

        public List<Pokemon> GetPokemonByName(string name)
        {
            var pokemon = _pokemonsCollection.Find(pokemon => pokemon.Name == name).ToList();
            return pokemon == null ? throw new Exception("Pokemon not found") : pokemon;
        }

        public Pokemon AddPokemon(Pokemon newPokemon)
        {
            if (string.IsNullOrEmpty(newPokemon.Id))
            {
                newPokemon.Id = ObjectId.GenerateNewId().ToString();
            }
            _pokemonsCollection.InsertOne(newPokemon);
            return newPokemon;
        }

        public Pokemon UpdatePokemon(string id, Pokemon updatedPokemon)
        {
            var result = _pokemonsCollection.ReplaceOne(pokemon => pokemon.Id == id, updatedPokemon);
            if (result.MatchedCount == 0)
            {
                throw new Exception("Pokemon not found");
            }
            return updatedPokemon;
        }

        public bool DeletePokemon(string id)
        {
            var result = _pokemonsCollection.DeleteOne(pokemon => pokemon.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new Exception("Pokemon not found");
            }
            return true;
        }
    }
}
