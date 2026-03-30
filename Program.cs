using InmobiliariaMinimalAPI.Datos;
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

app.MapGet("/api/propiedades", () =>
{
    return Results.Ok(DatoaPropiedad.ListaPropiedades);
}).WithOpenApi();

app.UseHttpsRedirection();

app.Run();


