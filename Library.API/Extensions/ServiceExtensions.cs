using System.Text;
using Library.Contracts;
using Library.Domain.Models;
using Library.Domain.Settings;
using Library.Repository;
using Library.Service;
using Library.Service.AuthorUseCases;
using Library.Service.BookUseCases;
using Library.Service.ImageUseCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Library.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
        });

    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

    public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ImageSettings>(configuration.GetSection("ImageSettings"));
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services)
        => services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services)
        => services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureUseCases(this IServiceCollection services)
    {
        services.AddScoped<GetAuthorsUseCase>();
        services.AddScoped<GetAuthorByIdUseCase>();
        services.AddScoped<CreateAuthorUseCase>();
        services.AddScoped<UpdateAuthorUseCase>();
        services.AddScoped<DeleteAuthorUseCase>();

        services.AddScoped<GetBooksUseCase>();
        services.AddScoped<GetBookByIdUseCase>();
        services.AddScoped<GetBookByIsbnUseCase>();
        services.AddScoped<GetBooksByAuthorIdUseCase>();
        services.AddScoped<CreateBookUseCase>();
        services.AddScoped<UpdateBookUseCase>();
        services.AddScoped<DeleteBookUseCase>();

        services.AddScoped<DownloadImageUseCase>();
        services.AddScoped<UploadImageUseCase>();
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Library API",
                Version = "v1",
                Description = "Library API for a traineeship",
                Contact = new OpenApiContact
                {
                    Name = "Vladislav Lashkin",
                    Email = "lashkin2004@gmail.com",
                    Url = new Uri("https://t.me/vlados1kin"),
                },
            });

            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            s.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer"
                    },
                    new List<string>()
                }
            });
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, IdentityRole<Guid>>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
    }

    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secret = configuration["JwtSettings:SecretKey"];

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };
            });
    }
}