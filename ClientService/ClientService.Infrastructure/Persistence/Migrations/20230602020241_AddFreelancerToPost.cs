using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFreelancerToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FreelancerId",
                table: "Posts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TransactionId",
                table: "Posts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_FreelancerId",
                table: "Posts",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TransactionId",
                table: "Posts",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Accounts_FreelancerId",
                table: "Posts",
                column: "FreelancerId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Transactions_TransactionId",
                table: "Posts",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Accounts_FreelancerId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Transactions_TransactionId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_FreelancerId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_TransactionId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "FreelancerId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Posts");
        }
    }
}
