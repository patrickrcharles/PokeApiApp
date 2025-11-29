using PokemonEffectivenessApp.src.Models;
using System.Net.Http.Json;

namespace PokemonEffectivenessApp.src.ApiClient
{
    public class PokeApiClient(HttpClient http) : IPokeApiClient
    {
        private readonly HttpClient _http = http;
        private readonly string _baseUrl = "https://pokeapi.co/api/v2";

        public async Task<PokemonDto?> GetPokemonAsync(string name)
        {
            var url = new Uri(_baseUrl + $"/pokemon/{name}");
            try
            {
                var res = await _http.GetAsync(url);
                if (!res.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to fetch Pokémon '{name}'. Status code: {res.StatusCode}");

                var content = await res.Content.ReadFromJsonAsync<PokemonDto>();
                return content;
            }
            catch (Exception ex) { 
                Console.WriteLine($"Network Error : {ex.Message}");
                return default;
            }
        }

        public async Task<TypesDto?> GetTypeByUrlAsync(string url)
        {
            try
            {
                var res = await _http.GetAsync(url.Trim());
                if (!res.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to fetch Type from URL '{url}', status code: {res.StatusCode}");

                var content = await res.Content.ReadFromJsonAsync<TypesDto>();
                return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Network Error : {ex.Message}");
                return default;
            }
        }
    }
}
