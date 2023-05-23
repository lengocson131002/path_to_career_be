using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Accounts_AcceptedAccountId",
                table: "Posts");

            migrationBuilder.AlterColumn<long>(
                name: "AcceptedAccountId",
                table: "Posts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Accounts_AcceptedAccountId",
                table: "Posts",
                column: "AcceptedAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Accounts_AcceptedAccountId",
                table: "Posts");

            migrationBuilder.AlterColumn<long>(
                name: "AcceptedAccountId",
                table: "Posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Accounts_AcceptedAccountId",
                table: "Posts",
                column: "AcceptedAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
