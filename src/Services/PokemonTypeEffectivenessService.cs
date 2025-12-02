using PokeApiApp.Models;

namespace PokeApiApp.Services;

public class PokemonTypeEffectivenessService : IPokemonTypeEffectivenessService
{
    public void ApplyTypeRelations(TypesDto dto, CombinedEffectivenessDTO combined)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(combined);

        // Extract the type's damage relation info once to avoid repeatedly accessing dto.DamageRelations.
        var r = dto.DamageRelations;

        // === STRONG AGAINST ===
        foreach (var x in r.DoubleDamageTo)
            combined.StrongAgainst.Add(x.Name);

        foreach (var x in r.NoDamageFrom)
            combined.StrongAgainst.Add(x.Name);

        foreach (var x in r.HalfDamageFrom)
            combined.StrongAgainst.Add(x.Name);

        // === WEAK AGAINST ===
        foreach (var x in r.NoDamageTo)
            combined.WeakAgainst.Add(x.Name);

        foreach (var x in r.HalfDamageTo)
            combined.WeakAgainst.Add(x.Name);

        foreach (var x in r.DoubleDamageFrom)
            combined.WeakAgainst.Add(x.Name);
    }
}

