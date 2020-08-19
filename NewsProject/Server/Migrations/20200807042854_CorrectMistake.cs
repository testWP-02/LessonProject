using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsProject.Server.Migrations
{
    public partial class CorrectMistake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatabaseImages_Images_ImageId",
                table: "DatabaseImages");

            migrationBuilder.DropForeignKey(
                name: "FK_DatabaseImages_News_NewsId",
                table: "DatabaseImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_News_NewsId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsManagers_News_NewsId",
                table: "ProductsManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsManagers_People_PersonId",
                table: "ProductsManagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsManagers",
                table: "ProductsManagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DatabaseImages",
                table: "DatabaseImages");

            migrationBuilder.RenameTable(
                name: "ProductsManagers",
                newName: "NewsManagers");

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "NewsCategories");

            migrationBuilder.RenameTable(
                name: "DatabaseImages",
                newName: "NewsImages");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsManagers_PersonId",
                table: "NewsManagers",
                newName: "IX_NewsManagers_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "NewsCategories",
                newName: "IX_NewsCategories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_DatabaseImages_ImageId",
                table: "NewsImages",
                newName: "IX_NewsImages_ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsManagers",
                table: "NewsManagers",
                columns: new[] { "NewsId", "PersonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsCategories",
                table: "NewsCategories",
                columns: new[] { "NewsId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsImages",
                table: "NewsImages",
                columns: new[] { "NewsId", "ImageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NewsCategories_Categories_CategoryId",
                table: "NewsCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsCategories_News_NewsId",
                table: "NewsCategories",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImages_Images_ImageId",
                table: "NewsImages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImages_News_NewsId",
                table: "NewsImages",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsManagers_News_NewsId",
                table: "NewsManagers",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsManagers_People_PersonId",
                table: "NewsManagers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsCategories_Categories_CategoryId",
                table: "NewsCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsCategories_News_NewsId",
                table: "NewsCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsImages_Images_ImageId",
                table: "NewsImages");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsImages_News_NewsId",
                table: "NewsImages");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsManagers_News_NewsId",
                table: "NewsManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsManagers_People_PersonId",
                table: "NewsManagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsManagers",
                table: "NewsManagers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsImages",
                table: "NewsImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsCategories",
                table: "NewsCategories");

            migrationBuilder.RenameTable(
                name: "NewsManagers",
                newName: "ProductsManagers");

            migrationBuilder.RenameTable(
                name: "NewsImages",
                newName: "DatabaseImages");

            migrationBuilder.RenameTable(
                name: "NewsCategories",
                newName: "ProductCategories");

            migrationBuilder.RenameIndex(
                name: "IX_NewsManagers_PersonId",
                table: "ProductsManagers",
                newName: "IX_ProductsManagers_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsImages_ImageId",
                table: "DatabaseImages",
                newName: "IX_DatabaseImages_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsCategories_CategoryId",
                table: "ProductCategories",
                newName: "IX_ProductCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsManagers",
                table: "ProductsManagers",
                columns: new[] { "NewsId", "PersonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DatabaseImages",
                table: "DatabaseImages",
                columns: new[] { "NewsId", "ImageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                columns: new[] { "NewsId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DatabaseImages_Images_ImageId",
                table: "DatabaseImages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DatabaseImages_News_NewsId",
                table: "DatabaseImages",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_News_NewsId",
                table: "ProductCategories",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsManagers_News_NewsId",
                table: "ProductsManagers",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsManagers_People_PersonId",
                table: "ProductsManagers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
