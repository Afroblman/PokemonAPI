using PokemonAPIApplication.Models;

namespace PokemonAPIApplication.Services
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> GetPokemons();
        Task<Pokemon> GetPokemonById(string id);
        Task<List<Pokemon>> GetPokemonByName(string name);
        Task<Pokemon> AddPokemon(Pokemon newPokemon);
        Task<Pokemon> UpdatePokemon(string id, Pokemon updatedPokemon);
        Task<bool> DeletePokemon(string id);
    }
}
