
using Microsoft.EntityFrameworkCore;
using SistemaRegistroEstudiantes.Data;
using SistemaRegistroEstudiantes.Modules.Estudiantes.Entities;

namespace SistemaRegistroEstudiantes.Modules.Estudiantes.Repositories;

public class EstudiantesRepository
{
    private readonly AppDbContext _context;

    public EstudiantesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Estudiante>> ObtenerTodos()
    {
        return await _context.Estudiantes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Estudiante?> ObtenerPorId(Guid id)
    {
        return await _context.Estudiantes
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Estudiante?> ObtenerPorMatricula(string matricula)
    {
        return await _context.Estudiantes
            .FirstOrDefaultAsync(e => e.Matricula == matricula);
    }

    public async Task<List<Estudiante>> ObtenerPorEdad(int edad)
    {
        return await _context.Estudiantes
            .AsNoTracking()
            .Where(e => e.Edad > edad)
            .ToListAsync();
    }

    public async Task Crear(Estudiante estudiante)
    {
        await _context.Estudiantes.AddAsync(estudiante);
        await _context.SaveChangesAsync();
    }

    public async Task Actualizar(Estudiante estudiante)
    {
        _context.Estudiantes.Update(estudiante);
        await _context.SaveChangesAsync();
    }

    public async Task Eliminar(Estudiante estudiante)
    {
        _context.Estudiantes.Remove(estudiante);
        await _context.SaveChangesAsync();
    }
}
