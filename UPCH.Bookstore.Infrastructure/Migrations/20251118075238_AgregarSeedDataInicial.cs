using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPCH.Bookstore.Infrastructure.Migrations
{
    public partial class AgregarSeedDataInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ===================================
            // 1. INSERCIÓN EN TABLA CATEGORIA
            // ===================================
            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Nombre","UsuarioCreacion","FechaCreacion" },
                values: new object[,]
                {
                    { 1, "Ficción","system",DateTime.Now },
                    {2, "Ciencia Ficción", "system", DateTime.Now},
                    {3, "Novela Histórica", "system", DateTime.Now},
                    {4, "Biografía", "system", DateTime.Now}
                });

            // ===================================
            // 2. INSERCIÓN EN TABLA EDITORIAL
            // ===================================
            migrationBuilder.InsertData(
                table: "Editorial",
                columns: new[] { "Id", "Nombre", "UsuarioCreacion", "FechaCreacion" },
                values: new object[,]
                {
                    {1, "Editorial Planeta", "system", DateTime.Now},
                    {2, "Penguin Random House", "system", DateTime.Now},
                    {3, "Fondo de Cultura Económica", "system", DateTime.Now}
                });

            // ===================================
            // 3. INSERCIÓN EN TABLA AUTOR
            // ===================================
            migrationBuilder.InsertData(
                table: "Autor",
                columns: new[] { "Id", "Nombre", "UsuarioCreacion", "FechaCreacion" },
                values: new object[,]
                {
                    { 1, "Gabriel García Márquez","system",DateTime.Now},
                    {2, "Isabel Allende", "system", DateTime.Now},
                    {3, "George Orwell", "system", DateTime.Now}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // ===================================
            // REVERSIÓN (OPCIONAL, PERO RECOMENDADA)
            // ===================================
            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4 });

            migrationBuilder.DeleteData(
                table: "Editorial",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 });

            migrationBuilder.DeleteData(
                table: "Autor",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 });
        }
    }
}