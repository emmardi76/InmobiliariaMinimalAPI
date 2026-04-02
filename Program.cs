using InmobiliariaMinimalAPI.Modelos;
using InmobiliariaMinimalAPI.Datos;
using InmobiliariaMinimalAPI.Modelos.DTOS;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaMinimalAPI.Mapper;
using Scalar.AspNetCore;
using AutoMapper;
using FluentValidation;
using InmobiliariaMinimalAPI.Validaciones;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(ConfiguracionDeMapper));

//Configurar validaciones con FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ValidacionCrearPropiedad>();

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
    RespuestasAPI respuesta = new();   
    //usar el logger que ya está como inyección de dependencias
    //para mostrar un mensaje en la consola cada vez que se accede a esta ruta
    logger.LogInformation("Se ha accedido a la ruta /api/propiedades para obtener todas las propiedades.");

    respuesta.Resultado = DatosPropiedad.ListaPropiedades;
    respuesta.Success = true;
    respuesta.CodigoDeEstado = HttpStatusCode.OK;
    return Results.Ok(respuesta);
}).WithName("ObtenerPropiedades").Produces<RespuestasAPI>(200).WithOpenApi();

//Obtener propiedad individual -GET- MapGet
app.MapGet("/api/propiedades/{id:int}", (int id) =>
{
    RespuestasAPI respuesta = new();

    respuesta.Resultado = DatosPropiedad.ListaPropiedades.FirstOrDefault(p => p.IdPropiedad == id);
    respuesta.Success = true;
    respuesta.CodigoDeEstado = HttpStatusCode.OK;
    return Results.Ok(respuesta);
   
}).WithName("ObtenerPropiedad").Produces<RespuestasAPI>(200).WithOpenApi();

//Agregar nueva propiedad -POST- MapPost
app.MapPost("/api/propiedades", async (IMapper _mapper,
    IValidator<CrearPropiedadDTO> _validator, [FromBody] CrearPropiedadDTO crearPropiedadDto) =>
{
    RespuestasAPI respuesta = new() { Success = false,CodigoDeEstado = HttpStatusCode.BadRequest};

    var resultadoValidaciones = await _validator.ValidateAsync(crearPropiedadDto);
   
    //Validar que el id de propiedad y que el nombre no esté vacio
    if (!resultadoValidaciones.IsValid)
    {
        respuesta.Errores.Add(resultadoValidaciones.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(respuesta);
    }

    //Validar si el nombre de la propiedad ya existe en la lista
    if (DatosPropiedad.ListaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == crearPropiedadDto.Nombre.ToLower()) != null)
    {
        respuesta.Errores.Add("El nombre de la propiedad ya existe.");
        return Results.BadRequest(respuesta);
    }
     
    Propiedad propiedad = _mapper.Map<Propiedad>(crearPropiedadDto);

    propiedad.IdPropiedad = DatosPropiedad.ListaPropiedades.OrderByDescending
    (p => p.IdPropiedad).FirstOrDefault()?.IdPropiedad + 1 ?? 1;
    DatosPropiedad.ListaPropiedades.Add(propiedad);    

    PropiedadDTO propiedadDTO = _mapper.Map<PropiedadDTO>(propiedad);

    //return Results.CreatedAtRoute("ObtenerPropiedad", new { id=propiedad.IdPropiedad}, propiedadDTO);

    respuesta.Resultado = propiedadDTO;
    respuesta.Success = true;
    respuesta.CodigoDeEstado = HttpStatusCode.Created;
    return Results.Ok(respuesta);

}).WithName("CrearPropiedad").Accepts<CrearPropiedadDTO>("application/json").Produces<RespuestasAPI>(201).Produces(400).WithOpenApi();

app.UseHttpsRedirection();

app.Run();





