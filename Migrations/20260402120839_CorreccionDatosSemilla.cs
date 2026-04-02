using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InmobiliariaMinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionDatosSemilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Propiedad",
                columns: table => new
                {
                    IdPropiedad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedad", x => x.IdPropiedad);
                });

            migrationBuilder.InsertData(
                table: "Propiedad",
                columns: new[] { "IdPropiedad", "Activa", "Descripcion", "FechaCreacion", "Nombre", "Ubicacion" },
                values: new object[,]
                {
                    { 1, true, "Una hermosa casa ubicada en el centro de la ciudad.", new DateTime(2026, 4, 2, 13, 50, 0, 0, DateTimeKind.Unspecified), "Casa en el centro", "Calle Principal 123" },
                    { 2, true, "Un moderno apartamento con una vista espectacular al mar.", new DateTime(2026, 4, 2, 13, 50, 0, 0, DateTimeKind.Unspecified), "Apartamento con vista al mar", "Avenida del Mar 456" },
                    { 3, false, "Una encantadora casa de campo rodeada de naturaleza.", new DateTime(2026, 4, 2, 13, 50, 0, 0, DateTimeKind.Unspecified), "Casa de campo", "Camino Rural 789" },
                    { 4, true, "Un piso moderno ubicado en una zona residencial tranquila.", new DateTime(2026, 4, 2, 13, 50, 0, 0, DateTimeKind.Unspecified), "Piso en zona residencial", "Barrio Residencial 321" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Propiedad");
        }
    }
}
