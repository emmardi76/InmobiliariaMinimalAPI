using InmobiliariaMinimalAPI.Datos;
using InmobiliariaMinimalAPI.Modelos;
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

//First  Endpoints
//app.MapGet("/hello/{id:int}", (int id) =>
//{
//    //return "Hello World!";
//    //return Results.BadRequest("error generado en ejecuación");
//    return Results.Ok("El id es: " + id);
//}).WithOpenApi();
//app.MapPost("/echo", (string message) => message).WithOpenApi();

//Obtener todas las propiedades -GET -MapGet
app.MapGet("/api/propiedades", () =>
{
    return Results.Ok(DatosPropiedad.ListaPropiedades);
}).WithOpenApi();

//Obtener propiedad individual -GET- MapGet
app.MapGet("/api/propiedades/{id:int}", (int id) =>
{
    return Results.Ok(DatosPropiedad.ListaPropiedades.FirstOrDefault(p => p.IdPropiedad == id));
}).WithOpenApi();

//Agregar nueva propiedad -POST- MapPost
app.MapPost("/api/propiedades", ([FromBody]Propiedad propiedad) =>
{
    //Validar que el id de propiedad y que el nombre no esté vacio
    if (propiedad.IdPropiedad != 0 || string.IsNullOrEmpty(propiedad.Nombre))
    {
        return Results.BadRequest("El id de la propiedad no es correcto o el nombre está vacío.");
    }

    //Validar si el nombre de la propiedad ya existe en la lista
    if (DatosPropiedad.ListaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == propiedad.Nombre.ToLower()) != null)
    {
        return Results.BadRequest("El nombre de la propiedad ya existe.");
    }

    propiedad.IdPropiedad = DatosPropiedad.ListaPropiedades.Count > 0 ? DatosPropiedad.ListaPropiedades.Max(p => p.IdPropiedad) + 1 : 1;
    DatosPropiedad.ListaPropiedades.Add(propiedad);
    return Results.Ok(DatosPropiedad.ListaPropiedades);
}).WithOpenApi();

app.UseHttpsRedirection();

app.Run();


