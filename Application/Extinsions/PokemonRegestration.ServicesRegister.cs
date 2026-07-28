using Application.Contracts;
using Application.Services.Countries;
using Application.Services.Owners;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extinsions;

public static partial class PokemonRegestration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<ICountryAppService, CountryAppService>();
        services.AddTransient<IOwnerAppService, OwnerAppService>();
        return services;
    }
}
