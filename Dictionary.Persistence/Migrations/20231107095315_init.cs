using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dictionary.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dictionaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RussianWord = table.Column<string>(type: "text", nullable: false),
                    RussianTranslation = table.Column<string>(type: "text", nullable: false),
                    GermanTranslation = table.Column<string>(type: "text", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Photo = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionaries", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dictionaries");
        }
    }
}
