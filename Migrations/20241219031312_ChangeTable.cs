using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageRecognition.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCode",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ProcessedA",
                table: "Images",
                newName: "ProcessedAt");

            migrationBuilder.RenameColumn(
                name: "ImageBase64",
                table: "Images",
                newName: "CostumerCode");

            migrationBuilder.AddColumn<int>(
                name: "MeasurementType",
                table: "Images",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeasurementType",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ProcessedAt",
                table: "Images",
                newName: "ProcessedA");

            migrationBuilder.RenameColumn(
                name: "CostumerCode",
                table: "Images",
                newName: "ImageBase64");

            migrationBuilder.AddColumn<string>(
                name: "CustomerCode",
                table: "Images",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
