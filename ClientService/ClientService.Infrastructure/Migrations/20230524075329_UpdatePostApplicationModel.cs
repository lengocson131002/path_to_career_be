using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostApplicationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExperienceDescription",
                table: "PostApplications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FeePerCount",
                table: "PostApplications",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "MethodDescription",
                table: "PostApplications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SupportCount",
                table: "PostApplications",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExperienceDescription",
                table: "PostApplications");

            migrationBuilder.DropColumn(
                name: "FeePerCount",
                table: "PostApplications");

            migrationBuilder.DropColumn(
                name: "MethodDescription",
                table: "PostApplications");

            migrationBuilder.DropColumn(
                name: "SupportCount",
                table: "PostApplications");
        }
    }
}
