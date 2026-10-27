
using Microsoft.EntityFrameworkCore;
using SistemaRegistroEstudiantes.Data;
using SistemaRegistroEstudiantes.Modules.Estudiantes;
using SistemaRegistroEstudiantes.Modules.Estudiantes.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddEstudiantesModule();
builder.Services.AddControllers();

var app = builder.Build();


app.MapGet("/api", () =>
{
    return Results.Ok(new
    {
        message = "Bienvenido a mi API de estudiantes"
    });
});


app.MapGet("/api/estudiantes-por-edad", async (
    int edad,
    EstudiantesService service) =>
{
    var estudiantes = await service.ObtenerPorEdad(edad);

    return estudiantes.Count == 0
        ? Results.NotFound(new
        {
            Message = "No existen estudiantes con esa edad."
        })
        : Results.Ok(estudiantes);
});


app.MapControllers();

app.Run("http://localhost:1515");
