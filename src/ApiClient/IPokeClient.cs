using PokeApiApp.Models;

namespace PokeApiApp.ApiClient
{
    public interface IPokeApiClient
    {
        Task<PokemonDto?> GetPokemonAsync(string name);
        Task<TypesDto?> GetTypeByUrlAsync(string url);
    }

}
