using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostModelV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AcceptedAccountId",
                table: "Posts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AcceptedAccountId",
                table: "Posts",
                column: "AcceptedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AccountId",
                table: "Posts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Accounts_AcceptedAccountId",
                table: "Posts",
                column: "AcceptedAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Accounts_AccountId",
                table: "Posts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Accounts_AcceptedAccountId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Accounts_AccountId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AcceptedAccountId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AccountId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AcceptedAccountId",
                table: "Posts");
        }
    }
}
