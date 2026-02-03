using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class deleteProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos",
                schema: "CAT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CAT");

            migrationBuilder.CreateTable(
                name: "Productos",
                schema: "CAT",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpEliminacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IpModificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IpRegistro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NombreProducto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UsuarioEliminacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UsuarioRegistro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdProducto);
                });
        }
    }
}
