using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenLibrary.Web.Migrations
{
    public partial class AddedRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 80, nullable: false),
                    DocumentPath = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Summary = table.Column<string>(maxLength: 300, nullable: false),
                    PagesNumber = table.Column<int>(maxLength: 80, nullable: false),
                    GenderId = table.Column<int>(nullable: true),
                    AuthorId = table.Column<int>(nullable: true),
                    DocumentLanguageId = table.Column<int>(nullable: true),
                    TypeOfDocumentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentLanguages_DocumentLanguageId",
                        column: x => x.DocumentLanguageId,
                        principalTable: "DocumentLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_TypeOfDocuments_TypeOfDocumentId",
                        column: x => x.TypeOfDocumentId,
                        principalTable: "TypeOfDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AuthorId",
                table: "Documents",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentLanguageId",
                table: "Documents",
                column: "DocumentLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_GenderId",
                table: "Documents",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TypeOfDocumentId",
                table: "Documents",
                column: "TypeOfDocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
