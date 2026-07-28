using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extinsions;

public static partial class PokemonRegestration
{
    public static IServiceCollection AddPokemonServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add application services
        services.AddServices();
        // add repositories
        services.AddRepositories();
        return services;
    }
}