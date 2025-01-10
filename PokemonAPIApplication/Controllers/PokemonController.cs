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
        public List<Pokemon> Get()
        {
            return _pokemonService.GetPokemons();
        }
        [HttpGet("{id}")]
        public ActionResult GetPokemon(string id)
        {
            try
            {
                return Ok(_pokemonService.GetPokemonById(id));
            }
            catch (Exception)
            {
                return NotFound("Pokemon not found");
            }
        }
        [HttpGet("search/{name}")]
        public List<Pokemon> GetPokemonByName(string name)
        {
            return _pokemonService.GetPokemonByName(name);
        }

        [HttpPost]
        public Pokemon AddPokemon(Pokemon newPokemon)
        {
            return _pokemonService.AddPokemon(newPokemon);
        }
        [HttpPut("{id}")]
        public Pokemon UpdatePokemon(string id, Pokemon updatedPokemon)
        {
            return _pokemonService.UpdatePokemon(id, updatedPokemon);
        }

        [HttpDelete("{id}")]
        public bool DeletePokemon(string id)
        {
            return _pokemonService.DeletePokemon(id);
        }
    }
}
