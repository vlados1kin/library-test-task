using Library.Repository;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
}