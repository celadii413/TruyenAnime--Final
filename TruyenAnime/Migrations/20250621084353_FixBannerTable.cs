using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruyenAnime.Migrations
{
    /// <inheritdoc />
    public partial class FixBannerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Banners");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Banners",
                newName: "ButtonText");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Banners",
                newName: "BackgroundImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "Banners");

            migrationBuilder.RenameColumn(
                name: "ButtonText",
                table: "Banners",
                newName: "Link");

            migrationBuilder.RenameColumn(
                name: "BackgroundImageUrl",
                table: "Banners",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Banners",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Banners",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
