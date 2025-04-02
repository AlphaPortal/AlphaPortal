using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Extensions;

public static class ContextServiceExtensions
{
    public static IServiceCollection AddDataContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));
        return services;
    }
}
