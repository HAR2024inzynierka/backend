using Workshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Workshop.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Workshop.Core.Interfaces;
using Workshop.Core.Services;
using Workshop.Middleware;
using Microsoft.OpenApi.Models;
using Workshop.Filters;

var builder = WebApplication.CreateBuilder(args);

// Konfiguracja połączenia z bazą danych MySQL z użyciem Entity Framework Core.
builder.Services.AddDbContext<WorkshopDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Konfiguracja usługi autoryzacji JWT
var jwtSecret = builder.Configuration["Jwt:Secret"]; // Pobranie sekretnych danych JWT z konfiguracji
if (string.IsNullOrEmpty(jwtSecret))
{
    throw new InvalidOperationException("JWT secret is not configured.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Określenie schematu autoryzacji
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Określenie schematu wyzwań
})
.AddJwtBearer(options =>
{
    // Parametry walidacji tokenu JWT
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Walidacja nadawcy tokenu (można włączyć, jeśli wymagane)
        ValidateAudience = false, // Walidacja odbiorcy tokenu
        ValidateLifetime = true, // Walidacja okresu ważności tokenu
        ValidateIssuerSigningKey = true, // Walidacja klucza podpisującego
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)) // Klucz symetryczny dla JWT
    };
});

// Konfiguracja Swaggera
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Workshop", Version = "v1" }); // Ustawienie tytułu i wersji API

    // Dodanie wsparcia JWT do dokumentacji Swaggera
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization", // Nagłówek HTTP, w którym będzie przekazywany token
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // Określenie typu zabezpieczenia
        BearerFormat = "JWT", // Format tokenu
        In = ParameterLocation.Header, // Lokalizacja nagłówka
        Description = "Wprowadź token JWT do autoryzacji."
    });

    // Dodanie wymagań dla bezpieczeństwa w Swaggerze
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Dodanie usług do kontenera DI
builder.Services.AddControllers(); // Rejestracja kontrolerów
builder.Services.AddEndpointsApiExplorer(); // Rejestracja generatora API dla punktów końcowych
builder.Services.AddSwaggerGen(); // Rejestracja Swaggera

// Rejestracja repozytoriów, które będą wstrzykiwane do serwisów
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IAutoRepairShopRepository, AutoRepairShopRepository>();
builder.Services.AddScoped<IFavourRepository, FavourRepository>();
builder.Services.AddScoped<ITermRepository, TermRepository>();
builder.Services.AddScoped<IRecordRepository, RecordRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
// Rejestracja usług, które będą wykonywać logikę aplikacji
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAutoRepairShopService, AutoRepairShopService>();
builder.Services.AddScoped<IFavourService, FavourService>();
builder.Services.AddScoped<ITermService, TermService>();
builder.Services.AddScoped<IRecordService, RecordService>();

// Rejestracja dostępu do HttpContext
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Rejestracja niestandardowego filtra autoryzacji użytkownika
builder.Services.AddScoped<AuthorizeUserFilter>();

// Konfiguracja CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000"); // Dozwolona domena frontendowa
        policy.AllowAnyHeader(); // Zezwolenie na dowolne nagłówki
        policy.AllowAnyMethod(); // Zezwolenie na dowolne metody HTTP
    });
});

var app = builder.Build();

// Konfiguracja pipeline HTTP dla aplikacji
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Włączenie obsługi CORS
app.UseCors();
// Ustawienie routingu
app.UseRouting();


// Włączenie autentykacji i autoryzacji
app.UseAuthentication();
app.UseAuthorization();

// Middleware autoryzacji dla tras admina
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/admin"),
    appBuiler =>
    {
        appBuiler.UseMiddleware<AdminAuthorizationMiddleware>();
    });

// Mapowanie kontrolerów
app.MapControllers();

app.Run();
