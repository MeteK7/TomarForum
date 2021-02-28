using Microsoft.EntityFrameworkCore.Migrations;

namespace TomarForumData.Migrations
{
    public partial class AmountTotalPostAndUserAreCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountTotalPost",
                table: "Forums",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AmountTotalUser",
                table: "Forums",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountTotalPost",
                table: "Forums");

            migrationBuilder.DropColumn(
                name: "AmountTotalUser",
                table: "Forums");
        }
    }
}
