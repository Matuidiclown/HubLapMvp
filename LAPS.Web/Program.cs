using LAPS.Data.Core;
using LAPS.Data.Interfaces;
using LAPS.Data.Repositories;
using LAPS.Business.Interfaces;
using LAPS.Business.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // El origen de tu Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

// --- Servicios Base ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- Capa de Datos (Data) ---
// Registramos el acceso genérico como Singleton
builder.Services.AddSingleton<IDataAccess, SqlServerDataAccess>();

// Registramos los Repositorios
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

// --- Capa de Negocio (Business) ---
// Registramos los Servicios
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingService, BookingService>();

// Program.cs
builder.Services.AddSingleton<IDataAccess, SqlServerDataAccess>();

var app = builder.Build();

// --- Configuración del Pipeline ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularDev");
app.UseAuthorization();
app.MapControllers();

app.Run();