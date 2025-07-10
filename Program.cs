using Microsoft.EntityFrameworkCore;
using radar_api.Data;
using radar_api.Services;

var builder = WebApplication.CreateBuilder(args);

// === Connection string ===
var connectionString = Environment.GetEnvironmentVariable("RADAR_DB_CONNECTION")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<RadarDbContext>(options =>
    options.UseNpgsql(connectionString));

// === Services ===
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAssetService, AssetService>();

var app = builder.Build();

// === Middleware pipeline ===
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
