using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLogEntryAndLogTypeWithBaseAuditableEntityWithHasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => new { x.Id, x.LogTypeId });
                    table.ForeignKey(
                        name: "FK_LogEntries_LogTypes_LogTypeId",
                        column: x => x.LogTypeId,
                        principalTable: "LogTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LogTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Create" },
                    { 2, "Delete" },
                    { 3, "Update" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_LogTypeId",
                table: "LogEntries",
                column: "LogTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEntries");

            migrationBuilder.DropTable(
                name: "LogTypes");

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
        }
    }
}
