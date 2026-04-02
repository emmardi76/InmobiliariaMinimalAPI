using InmobiliariaMinimalAPI.Modelos;

namespace InmobiliariaMinimalAPI.Datos;

public static class DatosPropiedad
{
    public static List<Propiedad> ListaPropiedades { get; set; } =
    [
        new() {
            IdPropiedad = 1,
            Nombre = "Casa en el centro",
            Descripcion = "Una hermosa casa ubicada en el centro de la ciudad.",
            Ubicacion = "Calle Principal 123",
            Activa = true,
            FechaCreacion = DateTime.Now.AddDays(-10)
        },
        new() {
            IdPropiedad = 2,
            Nombre = "Apartamento con vista al mar",
            Descripcion = "Un moderno apartamento con una vista espectacular al mar.",
            Ubicacion = "Avenida del Mar 456",
            Activa = true,
            FechaCreacion = DateTime.Now.AddDays(-10)
        },
        new() {
            IdPropiedad = 3,
            Nombre = "Casa de campo",
            Descripcion = "Una encantadora casa de campo rodeada de naturaleza.",
            Ubicacion = "Camino Rural 789",
            Activa = false,
            FechaCreacion = DateTime.Now.AddDays(-10)
        },
        new() {
                IdPropiedad = 4,
                Nombre = "Piso en zona residencial",
                Descripcion = "Un piso moderno ubicado en una zona residencial tranquila.",
                Ubicacion = "Barrio Residencial 321",
                Activa = true,
                FechaCreacion = DateTime.Now.AddDays(-10)
        } 
    ];
}
