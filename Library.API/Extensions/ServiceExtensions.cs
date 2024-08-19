﻿using Library.Contracts;
using Library.Repository;
using Library.Service;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

    public static void ConfigureRepositoryManager(this IServiceCollection services)
        => services.AddScoped<IRepositoryManager, RepositoryManager>();
    
    public static void ConfigureServiceManager(this IServiceCollection services)
        => services.AddScoped<IServiceManager, ServiceManager>();
}