using LigueDimanche.Core.Interfaces;
using LigueDimanche.Infra;
using LigueDimanche.Infra.Repositories;
using LigueDimanche.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configuration Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LigueDimanche API",
        Version = "v1",
        Description = "API de gestion de la Ligue du Dimanche",
        Contact = new OpenApiContact
        {
            Name = "OUEL99",
            Email = "contact@example.com",
            Url = new Uri("https://github.com/OUEL99/LigueDimanche")
        }
    });

    // Inclure les commentaires XML pour la documentation
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Configuration pour les DTOs
    c.SchemaFilter<ExampleSchemaFilter>();
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injection des repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEquipeRepository, EquipeRepository>();

// Injection des services métier
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEquipeService, EquipeService>();

var app = builder.Build();

// Créer automatiquement la base de données si elle n'existe pas
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LigueDimanche API V1");
        c.RoutePrefix = string.Empty; // Swagger UI à la racine (/)
        c.DisplayRequestDuration();
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

// Classe pour personnaliser les exemples dans Swagger
public class ExampleSchemaFilter : Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter
{
    public void Apply(Microsoft.OpenApi.Models.OpenApiSchema schema, Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext context)
    {
        if (context.Type == typeof(LigueDimanche.Core.DTO.Users.CreateUserRequestDto))
        {
            schema.Example = new Microsoft.OpenApi.Any.OpenApiObject
            {
                ["email"] = new Microsoft.OpenApi.Any.OpenApiString("john.doe@example.com"),
                ["nom"] = new Microsoft.OpenApi.Any.OpenApiString("Doe"),
                ["prenom"] = new Microsoft.OpenApi.Any.OpenApiString("John"),
                ["motDePasse"] = new Microsoft.OpenApi.Any.OpenApiString("motdepassesecret123"),
                ["dateDeNaissance"] = new Microsoft.OpenApi.Any.OpenApiString("1995-06-15"),
                ["telephone"] = new Microsoft.OpenApi.Any.OpenApiString("0123456789"),
                ["estAdmin"] = new Microsoft.OpenApi.Any.OpenApiBoolean(false),
                ["positions"] = new Microsoft.OpenApi.Any.OpenApiArray
                {
                    new Microsoft.OpenApi.Any.OpenApiString("Attaquant"),
                    new Microsoft.OpenApi.Any.OpenApiString("Defenseur")
                }
            };
        }

        else if (context.Type == typeof(LigueDimanche.Core.DTO.Auth.LoginRequestDto))
        {
            schema.Example = new Microsoft.OpenApi.Any.OpenApiObject
            {
                ["email"] = new Microsoft.OpenApi.Any.OpenApiString("john.doe@example.com"),
                ["motDePasse"] = new Microsoft.OpenApi.Any.OpenApiString("motdepassesecret123")
            };
        }

        else if (context.Type == typeof(LigueDimanche.Core.DTO.Equipe.EquipeRequestDto))
        {
            schema.Example = new Microsoft.OpenApi.Any.OpenApiObject
            {
                ["nom"] = new Microsoft.OpenApi.Any.OpenApiString("Les Champions"),
                ["matchId"] = new Microsoft.OpenApi.Any.OpenApiInteger(1)
            };
        }

        else if (context.Type == typeof(LigueDimanche.Core.DTO.Equipe.EquipeResponseDto))
        {
            schema.Example = new Microsoft.OpenApi.Any.OpenApiObject
            {
                ["id"] = new Microsoft.OpenApi.Any.OpenApiInteger(1),
                ["nom"] = new Microsoft.OpenApi.Any.OpenApiString("Les Champions"),
                ["matchId"] = new Microsoft.OpenApi.Any.OpenApiInteger(1),
                ["joueurs"] = new Microsoft.OpenApi.Any.OpenApiArray
                {
                    new Microsoft.OpenApi.Any.OpenApiObject
                    {
                        ["id"] = new Microsoft.OpenApi.Any.OpenApiInteger(1),
                        ["email"] = new Microsoft.OpenApi.Any.OpenApiString("John.doe@example.com"),
                        ["nom"] = new Microsoft.OpenApi.Any.OpenApiString("Doe"),
                        ["prenom"] = new Microsoft.OpenApi.Any.OpenApiString("John"),
                        ["dateDeNaissance"] = new Microsoft.OpenApi.Any.OpenApiString("1995-06-15"),
                        ["telephone"] = new Microsoft.OpenApi.Any.OpenApiString("0123456789"),
                        ["estAdmin"] = new Microsoft.OpenApi.Any.OpenApiBoolean(false),
                        ["positions"] = new Microsoft.OpenApi.Any.OpenApiArray
                        {
                            new Microsoft.OpenApi.Any.OpenApiString("Attaquant"),
                            new Microsoft.OpenApi.Any.OpenApiString("Defenseur")
                        }
                    }
                }
            };
        }
    }
}
