using Microsoft.AspNetCore.Mvc;
using PokemonAPIApplication.Models;
using PokemonAPIApplication.Services;

namespace PokemonAPIApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            // inject the service into the controller
            _pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<List<Pokemon>> Get()
        {
            return await _pokemonService.GetPokemons();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPokemon(string id)
        {
            try
            {
                var pokemon = await _pokemonService.GetPokemonById(id);
                return Ok(pokemon);
            }
            catch (Exception)
            {
                return NotFound("Pokemon not found");
            }
        }

        [HttpGet("search/{name}")]
        public async Task<List<Pokemon>> GetPokemonByName(string name)
        {
            return await _pokemonService.GetPokemonByName(name);
        }

        [HttpPost]
        public async Task<Pokemon> AddPokemon(Pokemon newPokemon)
        {
            return await _pokemonService.AddPokemon(newPokemon);
        }

        [HttpPut("{id}")]
        public async Task<Pokemon> UpdatePokemon(string id, Pokemon updatedPokemon)
        {
            return await _pokemonService.UpdatePokemon(id, updatedPokemon);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeletePokemon(string id)
        {
            return await _pokemonService.DeletePokemon(id);
        }
    }
}
