using PokeApiApp.Models;

namespace PokeApiApp.Services;

public interface IPokemonTypeEffectivenessService
{
    void ApplyTypeRelations(TypesDto typeDto, CombinedEffectivenessDTO combined);
}

