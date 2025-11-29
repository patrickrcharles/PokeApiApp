using PokemonEffectivenessApp.src.Models;

namespace PokemonEffectivenessApp.src.Services;

public interface IPokemonTypeEffectivenessService
{
    void ApplyTypeRelations(TypesDto typeDto, CombinedEffectivenessDTO combined);
}

