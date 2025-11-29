namespace PokemonEffectivenessApp.src.Models
{
    public class CombinedEffectivenessDTO
    {
        public string Name { get; init; } = string.Empty;
        public IEnumerable<string> Types { get; init; } = [];
        public HashSet<string> StrongAgainst { get; } = new(StringComparer.OrdinalIgnoreCase);
        public HashSet<string> WeakAgainst { get; } = new(StringComparer.OrdinalIgnoreCase);
    }
}
