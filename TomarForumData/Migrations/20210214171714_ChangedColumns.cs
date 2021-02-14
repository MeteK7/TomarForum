using Microsoft.EntityFrameworkCore.Migrations;

namespace TomarForumData.Migrations
{
    public partial class ChangedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Posts",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "PostReplies",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "MemberSince",
                table: "AspNetUsers",
                newName: "MembershipCreatedOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Posts",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "PostReplies",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "MembershipCreatedOn",
                table: "AspNetUsers",
                newName: "MemberSince");
        }
    }
}
