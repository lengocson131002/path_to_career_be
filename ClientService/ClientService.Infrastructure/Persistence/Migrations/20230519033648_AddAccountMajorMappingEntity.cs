using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountMajorMappingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Accounts_AccountId",
                table: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_Majors_AccountId",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Majors");

            migrationBuilder.CreateTable(
                name: "AccountMajors",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    MajorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountMajors", x => new { x.AccountId, x.MajorId });
                    table.ForeignKey(
                        name: "FK_AccountMajors_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountMajors_Majors_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountMajors_MajorId",
                table: "AccountMajors",
                column: "MajorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountMajors");

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "Majors",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Majors_AccountId",
                table: "Majors",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_Accounts_AccountId",
                table: "Majors",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
