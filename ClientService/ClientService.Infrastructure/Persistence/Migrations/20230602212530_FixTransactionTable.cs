using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_TransactionId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "PaymentTime",
                table: "Transactions",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TransactionId",
                table: "Posts",
                column: "TransactionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_TransactionId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Transactions",
                newName: "PaymentTime");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TransactionId",
                table: "Posts",
                column: "TransactionId");
        }
    }
}
