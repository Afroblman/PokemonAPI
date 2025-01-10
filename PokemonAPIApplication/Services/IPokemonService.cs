using PokemonAPIApplication.Models;

namespace PokemonAPIApplication.Services
{
    public interface IPokemonService
    {
        List<Pokemon> GetPokemons();
        Pokemon GetPokemonById(string id);
        List<Pokemon> GetPokemonByName(string name);
        Pokemon AddPokemon(Pokemon newPokemon);
        Pokemon UpdatePokemon(string id, Pokemon updatedPokemon);
        bool DeletePokemon(string id);
    }
}
