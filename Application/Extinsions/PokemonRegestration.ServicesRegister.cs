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
        services.AddTransient<ICategoryAppService, Application.Services.Categories.CategoryAppService>();
        services.AddTransient<IPokemonAppService, Application.Services.Pokemons.PokemonAppService>();
        services.AddTransient<IReviewAppService, Application.Services.Reviews.ReviewAppService>();
        services.AddTransient<IReviewerAppService, Application.Services.Reviewers.ReviewerAppService>();
        return services;
    }
}
