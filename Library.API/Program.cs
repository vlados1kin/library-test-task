using Library.API.Extensions;
using Library.API.Middlewares;
using Library.API.Requirements;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.ConfigureSwagger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureCors();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureSettings(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServices();
builder.Services.ConfigureUseCases();

builder.Services.AddAutoMapper(typeof(Program));

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

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();