using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de la villa...", new DateTime(2023, 4, 10, 23, 9, 41, 695, DateTimeKind.Local).AddTicks(9202), new DateTime(2023, 4, 10, 23, 9, 41, 695, DateTimeKind.Local).AddTicks(9190), "", 200, "Villa Real", 5, 0.0 },
                    { 2, "", "Detalle de la villa...", new DateTime(2023, 4, 10, 23, 9, 41, 695, DateTimeKind.Local).AddTicks(9205), new DateTime(2023, 4, 10, 23, 9, 41, 695, DateTimeKind.Local).AddTicks(9205), "", 40, "Premium vista ala piscina", 4, 0.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
