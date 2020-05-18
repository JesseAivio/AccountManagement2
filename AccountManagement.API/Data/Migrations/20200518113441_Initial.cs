using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.API.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Subject = table.Column<string>(maxLength: 100, nullable: false),
                    Body = table.Column<string>(maxLength: 2000, nullable: false),
                    Type = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    BusinessId = table.Column<string>(maxLength: 9, nullable: false),
                    Info = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(maxLength: 30, nullable: false),
                    Application = table.Column<Guid>(nullable: false),
                    isFree = table.Column<bool>(nullable: false),
                    isLocked = table.Column<bool>(nullable: false),
                    RenewDate = table.Column<DateTime>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.Id);
                    table.ForeignKey(
                        name: "FK_License_Application_Application",
                        column: x => x.Application,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 40, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Salt = table.Column<string>(maxLength: 100, nullable: false),
                    Role = table.Column<string>(maxLength: 20, nullable: false),
                    Organization = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Organization_Organization",
                        column: x => x.Organization,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationOrganizations",
                columns: table => new
                {
                    Application = table.Column<Guid>(nullable: false),
                    Organization = table.Column<Guid>(nullable: false),
                    License = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationOrganizations", x => new { x.Application, x.Organization });
                    table.ForeignKey(
                        name: "FK_ApplicationOrganizations_Application_Application",
                        column: x => x.Application,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationOrganizations_License_License",
                        column: x => x.License,
                        principalTable: "License",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationOrganizations_Organization_Organization",
                        column: x => x.Organization,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Account = table.Column<Guid>(nullable: false),
                    Application = table.Column<Guid>(nullable: false),
                    wasSuccessful = table.Column<bool>(nullable: false),
                    HWID = table.Column<string>(maxLength: 50, nullable: false),
                    IpAddress = table.Column<string>(maxLength: 15, nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountLog_Account_Account",
                        column: x => x.Account,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountLog_Application_Application",
                        column: x => x.Application,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Organization",
                table: "Account",
                column: "Organization");

            migrationBuilder.CreateIndex(
                name: "IX_AccountLog_Account",
                table: "AccountLog",
                column: "Account");

            migrationBuilder.CreateIndex(
                name: "IX_AccountLog_Application",
                table: "AccountLog",
                column: "Application");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrganizations_License",
                table: "ApplicationOrganizations",
                column: "License");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrganizations_Organization",
                table: "ApplicationOrganizations",
                column: "Organization");

            migrationBuilder.CreateIndex(
                name: "IX_License_Application",
                table: "License",
                column: "Application");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountLog");

            migrationBuilder.DropTable(
                name: "ApplicationOrganizations");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "License");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "Application");
        }
    }
}
