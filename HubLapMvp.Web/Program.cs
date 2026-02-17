using HubLap.Business.Interfaces;
using HubLap.Business.Services;
using HubLap.Data.Core;
using HubLap.Data.Interfaces;
using HubLap.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios MVC
builder.Services.AddControllersWithViews();

// 2. Swagger (Opcional en MVC, pero útil si haces llamadas AJAX o API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Inyección de Dependencias
// Data Layer
builder.Services.AddSingleton<IDataAccess, SqlServerDataAccess>(); // Singleton suele ir bien para Dapper config wrapper
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
// Business Layer
builder.Services.AddScoped<IRoomService, RoomService>();
// Si tienes BookingService, agrégalo aquí también

// --- AGREGAR ESTO PARA LAS RESERVAS ---
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();

// --- ASEGÚRATE DE QUE ESTO TAMBIÉN ESTÉ (Para las salas) ---
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();

var app = builder.Build();

// 4. Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Ruta por defecto MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();