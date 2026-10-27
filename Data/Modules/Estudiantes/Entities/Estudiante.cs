
namespace SistemaRegistroEstudiantes.Modules.Estudiantes.Entities;

public record Estudiante
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Nombre { get; set; }

    public required string Apellido { get; set; }

    public required string Matricula { get; set; }

    public required string Carrera { get; set; }

    public required int Edad { get; set; }
}
