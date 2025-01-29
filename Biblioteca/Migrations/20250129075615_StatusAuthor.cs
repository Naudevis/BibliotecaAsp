using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class StatusAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status_id",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Status_id",
                table: "Authors",
                column: "Status_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Statuses_Status_id",
                table: "Authors",
                column: "Status_id",
                principalTable: "Statuses",
                principalColumn: "Status_id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Statuses_Status_id",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_Status_id",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Status_id",
                table: "Authors");
        }
    }
}
