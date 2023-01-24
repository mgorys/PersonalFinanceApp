using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinance.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class defaults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Default",
                table: "Categories");
        }
    }
}
