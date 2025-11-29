using System.Text.Json.Serialization;

namespace PokemonEffectivenessApp.src.Models
{
    public class PokemonDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("types")]
        public List<PokemonTypesList> Types { get; init; } = new();
    }

    public class PokemonTypesList
    {
        [JsonPropertyName("type")]
        public PokemonType Type { get; init; } = new();
    }

    public class PokemonType
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }

}
