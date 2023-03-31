using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EESTEC.Migrations
{
    /// <inheritdoc />
    public partial class AddEventTypeToLocalEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventType",
                table: "LocalEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventType",
                table: "LocalEvents");
        }
    }
}
