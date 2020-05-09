using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aiwins.BloggingTestApp.EntityFrameworkCore.Migrations
{
    public partial class Added_Identity_Module : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RocketPermissionGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderName = table.Column<string>(maxLength: 64, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RocketPermissionGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RocketRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RocketRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RocketSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(maxLength: 2048, nullable: false),
                    ProviderName = table.Column<string>(maxLength: 64, nullable: true),
                    ProviderKey = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RocketSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RocketUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(maxLength: 256, nullable: true),
                    SecurityStamp = table.Column<string>(maxLength: 256, nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 16, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false, defaultValue: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false, defaultValue: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(nullable: false, defaultValue: 0),
                    ExtraProperties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RocketUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RocketRoleClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    ClaimType = table.Column<string>(maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(maxLength: 1024, nullable: true),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RocketRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RocketRoleClaims_RocketRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RocketRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RocketUserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    ClaimType = table.Column<string>(maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(maxLength: 1024, nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RocketUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RocketUserClaims_RocketUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "RocketUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RocketUserLogins",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 64, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 196, nullable: false),
                    ProviderDisplayName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RocketUserLogins", x => new { x.UserId, x.LoginProvider });
                    table.ForeignKey(
                        name: "FK_RocketUserLogins_RocketUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "RocketUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RocketUserRoles",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RocketUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RocketUserRoles_RocketRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RocketRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RocketUserRoles_RocketUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "RocketUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RocketUserTokens",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RocketUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_RocketUserTokens_RocketUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "RocketUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RocketPermissionGrants_Name_ProviderName_ProviderKey",
                table: "RocketPermissionGrants",
                columns: new[] { "Name", "ProviderName", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_RocketRoleClaims_RoleId",
                table: "RocketRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RocketRoles_NormalizedName",
                table: "RocketRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_RocketSettings_Name_ProviderName_ProviderKey",
                table: "RocketSettings",
                columns: new[] { "Name", "ProviderName", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_RocketUserClaims_UserId",
                table: "RocketUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RocketUserLogins_LoginProvider_ProviderKey",
                table: "RocketUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_RocketUserRoles_RoleId_UserId",
                table: "RocketUserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_RocketUsers_Email",
                table: "RocketUsers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_RocketUsers_NormalizedEmail",
                table: "RocketUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_RocketUsers_NormalizedUserName",
                table: "RocketUsers",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_RocketUsers_UserName",
                table: "RocketUsers",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RocketPermissionGrants");

            migrationBuilder.DropTable(
                name: "RocketRoleClaims");

            migrationBuilder.DropTable(
                name: "RocketSettings");

            migrationBuilder.DropTable(
                name: "RocketUserClaims");

            migrationBuilder.DropTable(
                name: "RocketUserLogins");

            migrationBuilder.DropTable(
                name: "RocketUserRoles");

            migrationBuilder.DropTable(
                name: "RocketUserTokens");

            migrationBuilder.DropTable(
                name: "RocketRoles");

            migrationBuilder.DropTable(
                name: "RocketUsers");
        }
    }
}
