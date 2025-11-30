using Microsoft.Extensions.DependencyInjection;
using PokeApiApp.ApiClient;
using PokeApiApp.Models;
using PokeApiApp.Services;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        // Register services in the DI container
        var services = new ServiceCollection();
        services.AddSingleton<HttpClient>();
        services.AddSingleton<IPokeApiClient, PokeApiClient>();
        services.AddSingleton<IPokemonTypeEffectivenessService, PokemonTypeEffectivenessService>();
        // Build service provider and resolve services
        using var serviceProvider = services.BuildServiceProvider();
        var apiClient = serviceProvider.GetRequiredService<IPokeApiClient>();
        var effectivenessService = serviceProvider.GetRequiredService<IPokemonTypeEffectivenessService>();

        Console.Write("Enter Pokemon name or type 'exit' to quit\n");
        while (true)
        {
            try
            {
                Console.Write("\nEnter Pokemon name: ");
                var input = Console.ReadLine()?.Trim();
                // Validate input
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid Pokemon name.");
                    continue;
                }
                // Exit condition
                if (input.ToLower().Equals("exit"))
                {
                    Console.WriteLine("goodbye...");
                    break;
                }
                // Fetch Pokemon data asynchronously
                var pokemon = await apiClient.GetPokemonAsync(input.ToLower());
                
                if (pokemon == null || pokemon.Types.Count == 0)
                {
                    Console.WriteLine($"Pokemon '{input}' not found or has no types.");
                    continue;
                }
                // Prepare combined DTO to store computed effectiveness
                var combined = new CombinedEffectivenessDTO
                {
                    Name = pokemon.Name,
                    Types = pokemon.Types.Select(t => t.Type.Name)
                };
                // Fetch each type and apply effectiveness
                foreach (var t in pokemon.Types)
                {
                    var typeDto = await apiClient.GetTypeByUrlAsync(t.Type.Url);
                    if (typeDto != null) 
                        effectivenessService.ApplyTypeRelations(typeDto, combined);
                }
                // Display results
                DisplayEffectiveness(combined);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }

    private static void DisplayEffectiveness(CombinedEffectivenessDTO combined)
    {
        Console.WriteLine($"\n{combined.Name} : {string.Join(", ", combined.Types)}");
        Console.WriteLine($"Strong Against: {(combined.StrongAgainst.Any() ? string.Join(", ", combined.StrongAgainst) : "None")}");
        Console.WriteLine($"Weak Against: {(combined.WeakAgainst.Any() ? string.Join(", ", combined.WeakAgainst) : "None")}");
    }
}
