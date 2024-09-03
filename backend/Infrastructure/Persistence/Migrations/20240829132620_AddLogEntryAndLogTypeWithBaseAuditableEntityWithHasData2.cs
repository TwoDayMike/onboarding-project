using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLogEntryAndLogTypeWithBaseAuditableEntityWithHasData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumns: new[] { "TodoId", "TodoTypeId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumns: new[] { "TodoId", "TodoTypeId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "TodoTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TodoTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "TodoTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Software Udvikling" },
                    { 2, "Hjemmelige pligter" }
                });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "TodoId", "TodoTypeId", "Description", "IsCompleted", "Name", "UserId" },
                values: new object[,]
                {
                    { 2, 1, "Implementere en controller som lave en integration til IMDB", false, "Integration", null },
                    { 1, 2, "Hente vasketøj og gøre rent", false, "Husholdning", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumns: new[] { "TodoId", "TodoTypeId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumns: new[] { "TodoId", "TodoTypeId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "TodoTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TodoTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "TodoTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Software Udvikling" },
                    { 5, "Hjemmelige pligter" }
                });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "TodoId", "TodoTypeId", "Description", "IsCompleted", "Name", "UserId" },
                values: new object[,]
                {
                    { 2, 4, "Implementere en controller som lave en integration til IMDB", false, "Integration", null },
                    { 1, 5, "Hente vasketøj og gøre rent", false, "Husholdning", null }
                });
        }
    }
}
