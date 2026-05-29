using Microsoft.EntityFrameworkCore;
using TallerMecanico.CORE.Core.Interfaces;
using TallerMecanico.CORE.Infrastructure.Data;
using TallerMecanico.CORE.Infrastructure.Repositories;
using TallerMecanico.CORE.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Cadena de conexión a SQL Server (definida en appsettings.json).
var connectionString = builder.Configuration.GetConnectionString("DevConnection");

// Registro del DbContext con SQL Server.
builder.Services.AddDbContext<TallerMecanicoDbContext>(options =>
    options.UseSqlServer(connectionString));

// Inyección de dependencias del patrón Repository + Service (Pregunta 5).
// El controlador depende de la interfaz, no de la implementación concreta.
builder.Services.AddTransient<IOrdenServicioRepository, OrdenServicioRepository>();
builder.Services.AddTransient<IOrdenServicioService, OrdenServicioService>();

builder.Services.AddControllers();

// OpenAPI (documentación de la API en /openapi/v1.json en desarrollo).
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
