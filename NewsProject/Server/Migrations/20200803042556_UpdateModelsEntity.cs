using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsProject.Server.Migrations
{
    public partial class UpdateModelsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatabaseImages",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false),
                    NewsId = table.Column<int>(nullable: false),
                    Character = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseImages", x => new { x.NewsId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_DatabaseImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatabaseImages_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseImages_ImageId",
                table: "DatabaseImages",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatabaseImages");
        }
    }
}
