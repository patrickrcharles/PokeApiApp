using Microsoft.Extensions.DependencyInjection;
using PokeApiApp.ApiClient;
using PokeApiApp.Models;
using PokeApiApp.Services;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        // update services
        var services = new ServiceCollection();
        services.AddSingleton<HttpClient>();
        services.AddSingleton<IPokeApiClient, PokeApiClient>();
        services.AddSingleton<IPokemonTypeEffectivenessService, PokemonTypeEffectivenessService>();
        // dependency injection
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
                // check for bad input
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid Pokemon name.");
                    continue;
                }
                // exit
                if (input.ToLower().Equals("exit"))
                {
                    Console.WriteLine("goodbye...");
                    break;
                }
                
                var pokemon = await apiClient.GetPokemonAsync(input.ToLower());
                // verify pokemon input
                if (pokemon == null || pokemon.Types.Count == 0)
                {
                    Console.WriteLine($"Pokemon '{input}' not found or has no types.");
                    continue;
                }
                // get all pokemon types. pokemon can have multiple types
                var combined = new CombinedEffectivenessDTO
                {
                    Name = pokemon.Name,
                    Types = pokemon.Types.Select(t => t.Type.Name)
                };
                // get type relations for each pokemon type and apply effectivenesses
                foreach (var t in pokemon.Types)
                {
                    var typeDto = await apiClient.GetTypeByUrlAsync(t.Type.Url);
                    if (typeDto != null) 
                        effectivenessService.ApplyTypeRelations(typeDto, combined);
                }
                
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
