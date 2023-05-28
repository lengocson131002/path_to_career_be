using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostApplications",
                table: "PostApplications");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostApplications",
                table: "PostApplications",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PostApplications_PostId",
                table: "PostApplications",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostApplications",
                table: "PostApplications");

            migrationBuilder.DropIndex(
                name: "IX_PostApplications_PostId",
                table: "PostApplications");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostApplications",
                table: "PostApplications",
                columns: new[] { "PostId", "ApplierId" });
        }
    }
}
