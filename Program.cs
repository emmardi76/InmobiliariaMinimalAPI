using InmobiliariaMinimalAPI.Datos;
using InmobiliariaMinimalAPI.Modelos;
using InmobiliariaMinimalAPI.Modelos.DTOS;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//Obtener todas las propiedades -GET- MapGet ,
//se inyecta el logger (ejemplo de inyeccion de dependencias con minimal APi,
//si fuera un servicio personalizado habría que añadirlo previmente en la sección add services to container)
//para mostrar un mensaje en la consola cada vez que se accede a esta ruta
app.MapGet("/api/propiedades", (ILogger<Program> logger) =>
{ 
    //usar el logger que ya está como inyección de dependencias
    //para mostrar un mensaje en la consola cada vez que se accede a esta ruta
    logger.LogInformation("Se ha accedido a la ruta /api/propiedades para obtener todas las propiedades.");
    return Results.Ok(DatosPropiedad.ListaPropiedades);
}).WithName("ObtenerPropiedades").Produces<IEnumerable<Propiedad>>(200).WithOpenApi();

//Obtener propiedad individual -GET- MapGet
app.MapGet("/api/propiedades/{id:int}", (int id) =>
{
    return Results.Ok(DatosPropiedad.ListaPropiedades.FirstOrDefault(p => p.IdPropiedad == id));
}).WithName("ObtenerPropiedad").Produces<Propiedad>(200).WithOpenApi();

//Agregar nueva propiedad -POST- MapPost
app.MapPost("/api/propiedades", ([FromBody] CrearPropiedadDTO crearPropiedadDto) =>
{
    //Validar que el id de propiedad y que el nombre no esté vacio
    if (string.IsNullOrEmpty(crearPropiedadDto.Nombre))
    {
        return Results.BadRequest("El id de la propiedad no es correcto o el nombre está vacío.");
    }

    //Validar si el nombre de la propiedad ya existe en la lista
    if (DatosPropiedad.ListaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == crearPropiedadDto.Nombre.ToLower()) != null)
    {
        return Results.BadRequest("El nombre de la propiedad ya existe.");
    }

    Propiedad propiedad = new Propiedad
    {
        Nombre = crearPropiedadDto.Nombre,
        Descripcion = crearPropiedadDto.Descripcion,
        Ubicacion = crearPropiedadDto.Ubicacion,
        Activa = crearPropiedadDto.Activa
    };

    propiedad.IdPropiedad = DatosPropiedad.ListaPropiedades.OrderByDescending
    (p => p.IdPropiedad).FirstOrDefault()?.IdPropiedad + 1 ?? 1;
    DatosPropiedad.ListaPropiedades.Add(propiedad);
    //return Results.Ok(DatosPropiedad.ListaPropiedades);
    //return Results.Created($"/api/propiedades/{propiedad.IdPropiedad}", propiedad);

    PropiedadDTO propiedadDTO = new PropiedadDTO
    {
        IdPropiedad = propiedad.IdPropiedad,
        Nombre = propiedad.Nombre,
        Descripcion = propiedad.Descripcion,
        Ubicacion = propiedad.Ubicacion,
        Activa = propiedad.Activa
    };
    return Results.CreatedAtRoute("ObtenerPropiedad", new { id=propiedad.IdPropiedad}, propiedadDTO);
}).WithName("CrearPropiedad").Accepts<CrearPropiedadDTO>("application/json").Produces<PropiedadDTO>(201).Produces(400).WithOpenApi();

app.UseHttpsRedirection();

app.Run();


