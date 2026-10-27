
using Microsoft.EntityFrameworkCore;
using SistemaRegistroEstudiantes.Modules.Estudiantes.Entities;

namespace SistemaRegistroEstudiantes.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Matricula)
                .IsRequired()
                .HasMaxLength(30);

            entity.HasIndex(e => e.Matricula)
                .IsUnique();

            entity.Property(e => e.Carrera)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Edad)
                .IsRequired();
        });
    }
}
