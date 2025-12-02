using System.Text.Json.Serialization;

namespace PokeApiApp.Models
{
    public record TypesDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("damage_relations")]
        public TypeRelationsDto DamageRelations { get; init; } = new();
    }
}
