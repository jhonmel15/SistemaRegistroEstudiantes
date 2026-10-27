
using SistemaRegistroEstudiantes.Modules.Estudiantes.Repositories;
using SistemaRegistroEstudiantes.Modules.Estudiantes.Services;

namespace SistemaRegistroEstudiantes.Modules.Estudiantes;

public static class EstudiantesModule
{
    public static IServiceCollection AddEstudiantesModule(
        this IServiceCollection services)
    {
        services.AddScoped<EstudiantesRepository>();
        services.AddScoped<EstudiantesService>();

        return services;
    }
}
