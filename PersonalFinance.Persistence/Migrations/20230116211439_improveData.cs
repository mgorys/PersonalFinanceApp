using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinance.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class improveData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Transactions",
                newName: "Amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "Price");
        }
    }
}
