using System.Text.Json.Serialization;

namespace PokeApiApp.Models
{
    public record PokemonDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("types")]
        public List<PokemonTypesList> Types { get; init; } = new();
    }

    public record PokemonTypesList
    {
        [JsonPropertyName("type")]
        public PokemonType Type { get; init; } = new();
    }

    public record PokemonType
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }

}
