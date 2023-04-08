using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EESTEC.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountNumberToPartner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Partners");
        }
    }
}
