
using SistemaRegistroEstudiantes.Modules.Estudiantes.Dtos;
using SistemaRegistroEstudiantes.Modules.Estudiantes.Entities;
using SistemaRegistroEstudiantes.Modules.Estudiantes.Repositories;

namespace SistemaRegistroEstudiantes.Modules.Estudiantes.Services;

public class EstudiantesService
{
    private readonly EstudiantesRepository _repository;

    public EstudiantesService(EstudiantesRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Estudiante>> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Task<Estudiante?> ObtenerPorId(Guid id)
    {
        return _repository.ObtenerPorId(id);
    }

    public async Task<(Estudiante? Estudiante, string? Error)> Crear(
        CrearEstudianteDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre) ||
            string.IsNullOrWhiteSpace(dto.Apellido) ||
            string.IsNullOrWhiteSpace(dto.Matricula) ||
            string.IsNullOrWhiteSpace(dto.Carrera) ||
            dto.Edad <= 0)
        {
            return (null, "Todos los campos son obligatorios y la edad debe ser mayor que cero.");
        }

        var matriculaExistente =
            await _repository.ObtenerPorMatricula(dto.Matricula);

        if (matriculaExistente is not null)
        {
            return (null, "Ya existe un estudiante con esa matrícula.");
        }

        var estudiante = new Estudiante
        {
            Id = Guid.NewGuid(),
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Matricula = dto.Matricula,
            Carrera = dto.Carrera,
            Edad = dto.Edad
        };

        await _repository.Crear(estudiante);

        return (estudiante, null);
    }

    public async Task<(Estudiante? Estudiante, string? Error)> Actualizar(
        Guid id,
        ActualizarEstudianteDto dto)
    {
        var estudiante = await _repository.ObtenerPorId(id);

        if (estudiante is null)
        {
            return (null, "Estudiante no encontrado.");
        }

        if (dto.Nombre is not null)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return (null, "El nombre no puede estar vacío.");

            estudiante.Nombre = dto.Nombre;
        }

        if (dto.Apellido is not null)
        {
            if (string.IsNullOrWhiteSpace(dto.Apellido))
                return (null, "El apellido no puede estar vacío.");

            estudiante.Apellido = dto.Apellido;
        }

        if (dto.Matricula is not null)
        {
            if (string.IsNullOrWhiteSpace(dto.Matricula))
                return (null, "La matrícula no puede estar vacía.");

            var matriculaExistente =
                await _repository.ObtenerPorMatricula(dto.Matricula);

            if (matriculaExistente is not null &&
                matriculaExistente.Id != id)
            {
                return (null, "La matrícula ya pertenece a otro estudiante.");
            }

            estudiante.Matricula = dto.Matricula;
        }

        if (dto.Carrera is not null)
        {
            if (string.IsNullOrWhiteSpace(dto.Carrera))
                return (null, "La carrera no puede estar vacía.");

            estudiante.Carrera = dto.Carrera;
        }

        if (dto.Edad.HasValue)
        {
            if (dto.Edad.Value <= 0)
                return (null, "La edad debe ser mayor que cero.");

            estudiante.Edad = dto.Edad.Value;
        }

        await _repository.Actualizar(estudiante);

        return (estudiante, null);
    }

    public async Task<bool> Eliminar(Guid id)
    {
        var estudiante = await _repository.ObtenerPorId(id);

        if (estudiante is null)
        {
            return false;
        }

        await _repository.Eliminar(estudiante);

        return true;
    }

    public Task<List<Estudiante>> ObtenerPorEdad(int edad)
    {
        return _repository.ObtenerPorEdad(edad);
    }

    public async Task<(Estudiante? Estudiante, string? Error)> ActualizarCarrera(
        Guid id,
        string carrera)
    {
        var estudiante = await _repository.ObtenerPorId(id);

        if (estudiante is null)
        {
            return (null, "Estudiante no encontrado.");
        }

        if (string.IsNullOrWhiteSpace(carrera))
        {
            return (null, "La carrera es obligatoria.");
        }

        estudiante.Carrera = carrera;

        await _repository.Actualizar(estudiante);

        return (estudiante, null);
    }
}
