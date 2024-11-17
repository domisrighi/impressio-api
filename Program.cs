using System.Reflection;
using System.Text;
using ImpressioApi_.Application.Commands.ObraArte.Profile;
using ImpressioApi_.Application.Commands.ObraArteFavorita.Profile;
using ImpressioApi_.Application.Commands.Usuario.Profile;
using ImpressioApi_.Domain.Interfaces.Queries;
using ImpressioApi_.Domain.Interfaces.Repositories;
using ImpressioApi_.Infrastructure.Data.Contexts;
using ImpressioApi_.Infrastructure.Data.Queries;
using ImpressioApi_.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

#region INICIALIZANDO O BANCO DE DADOS
var connectionString = builder.Configuration["DatabaseConnection"];
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão do banco de dados deve ser fornecida nas variáveis de ambiente ou no appsettings.");
}

builder.Services.AddDbContext<ImpressioDbContext>(
    opt => opt.UseNpgsql(connectionString).EnableSensitiveDataLogging(true)
);
#endregion

// Configurando JWT
var jwtSecret = builder.Configuration["JwtSettings:Secret"];
if (string.IsNullOrEmpty(jwtSecret))
{
    throw new InvalidOperationException("O segredo JWT deve ser fornecido nas variáveis de ambiente ou no appsettings.");
}

var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//#endregion

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IObterUsuarioQuery, ObterUsuarioQuery>();

builder.Services.AddScoped<IObraArteRepository, ObraArteRepository>();
builder.Services.AddScoped<IObterObraArteQuery, ObterObraArteQuery>();

builder.Services.AddScoped<IObraArteFavoritaRepository, ObraArteFavoritaRepository>();
builder.Services.AddScoped<IObterObraArteFavoritaQuery, ObterObraArteFavoritaQuery>();

builder.Services.AddAutoMapper(typeof(UsuarioProfile));
builder.Services.AddAutoMapper(typeof(ObraArteProfile));
builder.Services.AddAutoMapper(typeof(ObraArteFavoritaProfile));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddSingleton<TokenService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no campo. Exemplo: Bearer {token}"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ImpressioDbContext>();
    dbContext.Database.Migrate();  // Aplica as migrações automaticamente
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
