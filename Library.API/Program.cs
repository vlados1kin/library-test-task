using Library.API.Extensions;
using Library.API.Middlewares;
using Library.API.Requirements;
using Library.Contracts;
using Library.Domain.Settings;
using Library.Service;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigureSwagger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureCors();

builder.Services.ConfigureDbContext(builder.Configuration);
//builder.Services.ConfigureRepositoryManager();
//builder.Services.ConfigureServiceManager();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureUseCases();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<ImageSettings>(builder.Configuration.GetSection("ImageSettings"));
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddSingleton<IAuthorizationHandler, SelfOnlyAuthorizationHandler>();

builder.Services.AddControllers();

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminAndSelfOnly", policy => policy.Requirements.Add(new SelfOnlyAuthorizationRequirement()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI(s => { s.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1"); });
}

//app.ConfigureExceptionHandler();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();