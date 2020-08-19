using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsProject.Server.Migrations
{
    public partial class AddAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO AspNetRoles (Id, [Name], NormalizedName)
                                 VALUES('a42c4fd4-6e85-4eaf-a575-881f1ea84593', 'Admin', 'Admin')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
