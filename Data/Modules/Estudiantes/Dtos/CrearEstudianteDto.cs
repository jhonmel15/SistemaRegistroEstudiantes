
namespace SistemaRegistroEstudiantes.Modules.Estudiantes.Dtos;

public class CrearEstudianteDto
{
    public required string Nombre { get; set; }

    public required string Apellido { get; set; }

    public required string Matricula { get; set; }

    public required string Carrera { get; set; }

    public required int Edad { get; set; }
}
