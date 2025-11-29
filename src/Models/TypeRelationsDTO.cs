using System.Text.Json.Serialization;

namespace PokeApiApp.Models
{
    public class TypeRelationsDto
    {
        [JsonPropertyName("double_damage_to")]
        public List<PokemonType> DoubleDamageTo { get; init; } = new();

        [JsonPropertyName("half_damage_to")]
        public List<PokemonType> HalfDamageTo { get; init; } = new();

        [JsonPropertyName("no_damage_to")]
        public List<PokemonType> NoDamageTo { get; init; } = new();

        [JsonPropertyName("double_damage_from")]
        public List<PokemonType> DoubleDamageFrom { get; init; } = new();

        [JsonPropertyName("half_damage_from")]
        public List<PokemonType> HalfDamageFrom { get; init; } = new();

        [JsonPropertyName("no_damage_from")]
        public List<PokemonType> NoDamageFrom { get; init; } = new();
    }
}
