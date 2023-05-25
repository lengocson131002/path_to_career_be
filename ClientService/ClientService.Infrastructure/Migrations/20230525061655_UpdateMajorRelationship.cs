using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMajorRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Posts_MajorId",
                table: "Posts",
                column: "MajorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Majors_MajorId",
                table: "Posts",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Majors_MajorId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_MajorId",
                table: "Posts");
        }
    }
}
