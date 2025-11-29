using PokeApiApp.Models;
using PokeApiApp.Services;

namespace PokemonEffectivenessTests.Tests.Services
{
    public class PokemonTypeEffectivenessServiceTests
    {
        private readonly PokemonTypeEffectivenessService _service;

        public PokemonTypeEffectivenessServiceTests()
        {
            _service = new PokemonTypeEffectivenessService();
        }

        private static TypesDto CreateDto(
            string name = "test-type",
            List<PokemonType>? doubleDamageTo = null,
            List<PokemonType>? noDamageFrom = null,
            List<PokemonType>? halfDamageFrom = null,
            List<PokemonType>? noDamageTo = null,
            List<PokemonType>? halfDamageTo = null,
            List<PokemonType>? doubleDamageFrom = null)
        {
            return new TypesDto
            {
                Name = name,
                DamageRelations = new TypeRelationsDto
                {
                    DoubleDamageTo = doubleDamageTo ?? new(),
                    NoDamageFrom = noDamageFrom ?? new(),
                    HalfDamageFrom = halfDamageFrom ?? new(),
                    NoDamageTo = noDamageTo ?? new(),
                    HalfDamageTo = halfDamageTo ?? new(),
                    DoubleDamageFrom = doubleDamageFrom ?? new()
                }
            };
        }

        private static PokemonType PT(string name) =>
            new()
            {
                Name = name,
                Url = $"https://pokeapi.co/api/v2/type/{name}"
            };

        private static CombinedEffectivenessDTO CreateCombined(string name = "result")
        {
            return new CombinedEffectivenessDTO
            {
                Name = name,
                Types = [] // not used in service logic
            };
        }

        [Fact]
        public void ApplyTypeRelations_Throws_On_Null_Dto()
        {
            var combined = CreateCombined();

            Assert.Throws<ArgumentNullException>(() =>
                _service.ApplyTypeRelations(null!, combined));
        }

        [Fact]
        public void ApplyTypeRelations_Throws_On_Null_Combined()
        {
            var dto = CreateDto();

            Assert.Throws<ArgumentNullException>(() =>
                _service.ApplyTypeRelations(dto, null!));
        }

        [Fact]
        public void ApplyTypeRelations_Adds_StrongAgainst()
        {
            var dto = CreateDto(
                name: "fire",
                doubleDamageTo: new() { PT("grass"), PT("ice") },
                noDamageFrom: new() { PT("ghost") },
                halfDamageFrom: new() { PT("water") }
            );

            var combined = CreateCombined();

            _service.ApplyTypeRelations(dto, combined);

            Assert.Contains("grass", combined.StrongAgainst);
            Assert.Contains("ice", combined.StrongAgainst);
            Assert.Contains("ghost", combined.StrongAgainst);
            Assert.Contains("water", combined.StrongAgainst);

            Assert.Empty(combined.WeakAgainst);
        }

        [Fact]
        public void ApplyTypeRelations_Adds_WeakAgainst()
        {
            var dto = CreateDto(
                name: "steel",
                noDamageTo: new() { PT("fairy") },
                halfDamageTo: new() { PT("rock") },
                doubleDamageFrom: new() { PT("electric") }
            );

            var combined = CreateCombined();

            _service.ApplyTypeRelations(dto, combined);

            Assert.Contains("fairy", combined.WeakAgainst);
            Assert.Contains("rock", combined.WeakAgainst);
            Assert.Contains("electric", combined.WeakAgainst);

            Assert.Empty(combined.StrongAgainst);
        }

        [Fact]
        public void ApplyTypeRelations_Ignores_Duplicates()
        {
            var dto = CreateDto(
                doubleDamageTo: new() { PT("grass"), PT("grass") },
                noDamageTo: new() { PT("poison"), PT("poison") }
            );

            var combined = CreateCombined();

            _service.ApplyTypeRelations(dto, combined);

            Assert.Single(combined.StrongAgainst);  // "grass"
            Assert.Single(combined.WeakAgainst);    // "poison"
        }
    }
}
