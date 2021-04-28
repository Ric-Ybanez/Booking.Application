using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking.Application.DAL.Migrations
{
    public partial class Third_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_UserLogins_CreatedById",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_UserLogins_UpdatedById",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CreatedById",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_UpdatedById",
                table: "Persons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Persons_CreatedById",
                table: "Persons",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_UpdatedById",
                table: "Persons",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_UserLogins_CreatedById",
                table: "Persons",
                column: "CreatedById",
                principalTable: "UserLogins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_UserLogins_UpdatedById",
                table: "Persons",
                column: "UpdatedById",
                principalTable: "UserLogins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
