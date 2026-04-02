using InmobiliariaMinimalAPI.Modelos;
using Microsoft.EntityFrameworkCore;

namespace InmobiliariaMinimalAPI.Datos;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Propiedad> Propiedad { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configuración de la entidad Propiedad
        modelBuilder.Entity<Propiedad>().HasData(
            new()
            {
                IdPropiedad = 1,
                Nombre = "Casa en el centro",
                Descripcion = "Una hermosa casa ubicada en el centro de la ciudad.",
                Ubicacion = "Calle Principal 123",
                Activa = true,
                FechaCreacion = new DateTime(2026, 4, 2, 13, 50, 0)
            },
            new()
            {
                IdPropiedad = 2,
                Nombre = "Apartamento con vista al mar",
                Descripcion = "Un moderno apartamento con una vista espectacular al mar.",
                Ubicacion = "Avenida del Mar 456",
                Activa = true,
                FechaCreacion = new DateTime(2026, 4, 2, 13, 50, 0)
            },
            new()
            {
                IdPropiedad = 3,
                Nombre = "Casa de campo",
                Descripcion = "Una encantadora casa de campo rodeada de naturaleza.",
                Ubicacion = "Camino Rural 789",
                Activa = false,
                FechaCreacion = new DateTime(2026, 4, 2, 13, 50, 0)
            },
            new()
            {
                IdPropiedad = 4,
                Nombre = "Piso en zona residencial",
                Descripcion = "Un piso moderno ubicado en una zona residencial tranquila.",
                Ubicacion = "Barrio Residencial 321",
                Activa = true,
                FechaCreacion = new DateTime(2026, 4, 2, 13, 50, 0)
            }
        );       
    }
}
