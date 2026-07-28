using Domain.Repositories;
using infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extinsions;

public static partial class PokemonRegestration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepository<,>), typeof(Repository<,>));
        return services;
    }
}
