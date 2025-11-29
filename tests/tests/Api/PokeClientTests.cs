using System.Net;
using System.Net.Http.Json;
using PokeApiApp.ApiClient;
using PokeApiApp.Models;

namespace PokemonEffectivenessTests.Tests.Api;

public class PokeApiClientTests
{
    [Fact]
    public async Task GetPokemonAsync_ReturnsPokemonDto_WhenSuccess()
    {
        // Arrange
        var expected = new PokemonDto
        {
            Name = "pikachu",
            Types =
            [
                new PokemonTypesList
                {
                    Type = new PokemonType { Name = "electric", Url = "url" }
                }
            ]
        };

        var httpClient = CreateMockHttpClient(
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(expected)
            });

        var client = new PokeApiClient(httpClient);

        // Act
        var result = await client.GetPokemonAsync("pikachu");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("pikachu", result.Name);
        Assert.Single(result.Types);
        Assert.Equal("electric", result.Types[0].Type.Name);
    }

    [Fact]
    public async Task GetPokemonAsync_ReturnsNull_WhenNotFound()
    {
        var httpClient = CreateMockHttpClient(new HttpResponseMessage(HttpStatusCode.NotFound));

        var client = new PokeApiClient(httpClient);

        var result = await client.GetPokemonAsync("unknown");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetTypeByUrlAsync_ReturnsTypeDto_WithCorrectNameAndDamageRelations()
    {
        var expected = new TypesDto
        {
            Name = "electric",
            DamageRelations = new TypeRelationsDto
            {
                DoubleDamageTo = { new PokemonType { Name = "water", Url = "url" } },
                NoDamageFrom = { new PokemonType { Name = "ground", Url = "url" } }
            }
        };

        var httpClient = CreateMockHttpClient(
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(expected)
            });

        var client = new PokeApiClient(httpClient);

        var result = await client.GetTypeByUrlAsync("https://pokeapi.co/api/v2/type/electric");

        Assert.NotNull(result);
        Assert.Equal("electric", result.Name);
        Assert.Single(result.DamageRelations.DoubleDamageTo);
        Assert.Equal("water", result.DamageRelations.DoubleDamageTo[0].Name);
        Assert.Single(result.DamageRelations.NoDamageFrom);
        Assert.Equal("ground", result.DamageRelations.NoDamageFrom[0].Name);
    }

    [Fact]
    public async Task GetTypeByUrlAsync_ReturnsNull_WhenNotFound()
    {
        var httpClient = CreateMockHttpClient(new HttpResponseMessage(HttpStatusCode.NotFound));

        var client = new PokeApiClient(httpClient);

        var result = await client.GetTypeByUrlAsync("https://pokeapi.co/api/v2/type/unknown");

        Assert.Null(result);
    }

    // --- Helper method to mock HttpClient ---
    private static HttpClient CreateMockHttpClient(HttpResponseMessage response)
    {
        var handler = new MockHttpMessageHandler(response);
        return new HttpClient(handler)
        {
            BaseAddress = new Uri("https://pokeapi.co/api/v2")
        };
    }

    // --- Minimal HttpMessageHandler mock ---
    private class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public MockHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }
}
