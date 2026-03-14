using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URL_Shortening.Migrations
{
    /// <inheritdoc />
    public partial class URLDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UrlOriginal = table.Column<string>(type: "TEXT", nullable: false),
                    UrlCorta = table.Column<string>(type: "TEXT", nullable: true),
                    Clicks = table.Column<int>(type: "INTEGER", nullable: false),
                    createAt = table.Column<int>(type: "INTEGER", nullable: false),
                    updateAt = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlMappings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlMappings");
        }
    }
}
