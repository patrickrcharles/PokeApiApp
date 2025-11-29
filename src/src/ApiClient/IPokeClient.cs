using PokemonEffectivenessApp.src.Models;

namespace PokemonEffectivenessApp.src.ApiClient
{
    public interface IPokeApiClient
    {
        Task<PokemonDto?> GetPokemonAsync(string name);
        Task<TypesDto?> GetTypeByUrlAsync(string url);
    }

}
