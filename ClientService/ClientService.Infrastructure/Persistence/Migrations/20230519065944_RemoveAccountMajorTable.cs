using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAccountMajorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountMajors");

            migrationBuilder.CreateTable(
                name: "AccountMajor",
                columns: table => new
                {
                    AccountsId = table.Column<long>(type: "bigint", nullable: false),
                    MajorsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountMajor", x => new { x.AccountsId, x.MajorsId });
                    table.ForeignKey(
                        name: "FK_AccountMajor_Accounts_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountMajor_Majors_MajorsId",
                        column: x => x.MajorsId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountMajor_MajorsId",
                table: "AccountMajor",
                column: "MajorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountMajor");

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
    }
}
