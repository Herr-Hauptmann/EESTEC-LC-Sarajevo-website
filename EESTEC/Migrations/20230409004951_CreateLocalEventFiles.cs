using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EESTEC.Migrations
{
    /// <inheritdoc />
    public partial class CreateLocalEventFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalEventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventFiles_LocalEvents_LocalEventId",
                        column: x => x.LocalEventId,
                        principalTable: "LocalEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventFiles_LocalEventId",
                table: "EventFiles",
                column: "LocalEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventFiles");
        }
    }
}
