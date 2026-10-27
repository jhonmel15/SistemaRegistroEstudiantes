
using Microsoft.AspNetCore.Mvc;
using SistemaRegistroEstudiantes.Modules.Estudiantes.Dtos;
using SistemaRegistroEstudiantes.Modules.Estudiantes.Services;

namespace SistemaRegistroEstudiantes.Controllers;

[ApiController]
[Route("api/estudiantes")]
public class EstudiantesController : ControllerBase
{
    private readonly EstudiantesService _service;

    public EstudiantesController(EstudiantesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var estudiantes = await _service.ObtenerTodos();
        return Ok(estudiantes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(Guid id)
    {
        var estudiante = await _service.ObtenerPorId(id);

        if (estudiante is null)
            return NotFound(new { Message = "Estudiante no encontrado" });

        return Ok(estudiante);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(CrearEstudianteDto dto)
    {
        var (estudiante, error) = await _service.Crear(dto);

        if (error is not null)
            return BadRequest(new { Message = error });

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = estudiante!.Id },
            estudiante);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> ActualizarParcialmente(
        Guid id,
        ActualizarEstudianteDto dto)
    {
        var (estudiante, error) = await _service.Actualizar(id, dto);

        if (error is not null)
        {
            if (error.Contains("no encontrado", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { Message = error });

            return BadRequest(new { Message = error });
        }

        return Ok(estudiante);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Eliminar(Guid id)
    {
        var eliminado = await _service.Eliminar(id);

        if (!eliminado)
            return NotFound(new { Message = "Estudiante no encontrado" });

        return Ok(new { Message = "Estudiante eliminado correctamente" });
    }
}
