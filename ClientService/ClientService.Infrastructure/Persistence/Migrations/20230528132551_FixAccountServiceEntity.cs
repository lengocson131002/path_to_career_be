using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixAccountServiceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "Services",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TransactionId",
                table: "AccountServices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AccountServices_TransactionId",
                table: "AccountServices",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountServices_Transactions_TransactionId",
                table: "AccountServices",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountServices_Transactions_TransactionId",
                table: "AccountServices");

            migrationBuilder.DropIndex(
                name: "IX_AccountServices_TransactionId",
                table: "AccountServices");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "AccountServices");

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "Services",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
