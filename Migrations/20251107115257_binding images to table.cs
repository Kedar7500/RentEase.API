using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentEase.API.Migrations
{
    /// <inheritdoc />
    public partial class bindingimagestotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PropertyId",
                table: "Images",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Properties_PropertyId",
                table: "Images",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Properties_PropertyId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PropertyId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Images");
        }
    }
}
