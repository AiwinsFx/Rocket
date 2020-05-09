using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aiwins.BloggingTestApp.EntityFrameworkCore.Migrations
{
    public partial class Identity_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BlgUsers",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "BlgUsers",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "RocketUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "RocketUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "RocketUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "RocketUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RocketUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "RocketUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "RocketUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RocketUsers",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "RocketUsers",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "RocketRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "RocketRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStatic",
                table: "RocketRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RocketClaimTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Required = table.Column<bool>(nullable: false),
                    IsStatic = table.Column<bool>(nullable: false),
                    Regex = table.Column<string>(maxLength: 512, nullable: true),
                    RegexDescription = table.Column<string>(maxLength: 128, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    ValueType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RocketClaimTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RocketClaimTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BlgUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "BlgUsers");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "RocketUsers");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "RocketUsers");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "RocketUsers");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "RocketUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RocketUsers");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "RocketUsers");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "RocketUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RocketUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "RocketUsers");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "RocketRoles");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "RocketRoles");

            migrationBuilder.DropColumn(
                name: "IsStatic",
                table: "RocketRoles");
        }
    }
}
