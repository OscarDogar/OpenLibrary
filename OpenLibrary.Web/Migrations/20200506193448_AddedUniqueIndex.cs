using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenLibrary.Web.Migrations
{
    public partial class AddedUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TypeOfDocuments_Name",
                table: "TypeOfDocuments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genders_Name",
                table: "Genders",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentLanguages_Name",
                table: "DocumentLanguages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Name",
                table: "Authors",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TypeOfDocuments_Name",
                table: "TypeOfDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Genders_Name",
                table: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_DocumentLanguages_Name",
                table: "DocumentLanguages");

            migrationBuilder.DropIndex(
                name: "IX_Authors_Name",
                table: "Authors");
        }
    }
}
