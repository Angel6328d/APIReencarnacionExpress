using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ReencarnacionExpress.Context;
using ReencarnacionExpress.Services.Interfaz;
using ReencarnacionExpress.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Base de datos (Supabase / PostgreSQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servicios
builder.Services.AddScoped<IEnemigoService, EnemigoService>();
builder.Services.AddScoped<IHistoriaService, HistoriaService>();
builder.Services.AddScoped<IPersonajeService, PersonajeService>();
builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddScoped<IVestigioService, VestigioService>();

// Archivos (Supabase Storage)
builder.Services.AddHttpClient<IArchivoService, SupabaseArchivoService>();

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ReencarnacionExpress v1");
    options.RoutePrefix = string.Empty; // Swagger en la raíz
});

// Render ya maneja el HTTPS, así que solo se redirige en local
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.Run();