using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketingCoinBase.Migrations
{
    public partial class RefTokenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountBalances",
                columns: table => new
                {
                    balanceID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    soldeBalance = table.Column<double>(type: "float", nullable: false),
                    soldeInvest = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBalances", x => x.balanceID);
                });

            migrationBuilder.CreateTable(
                name: "Commissions",
                columns: table => new
                {
                    commissionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    totMumbers = table.Column<double>(type: "float", nullable: false),
                    commission = table.Column<double>(type: "float", nullable: false),
                    datePeriod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commissions", x => x.commissionID);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    partnerID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    partnerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.partnerID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    roleID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.roleID);
                });

            migrationBuilder.CreateTable(
                name: "ServeProds",
                columns: table => new
                {
                    serveProdID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typeOfService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    partnerID = table.Column<long>(type: "bigint", nullable: false),
                    partnerspartnerID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServeProds", x => x.serveProdID);
                    table.ForeignKey(
                        name: "FK_ServeProds_Partners_partnerspartnerID",
                        column: x => x.partnerspartnerID,
                        principalTable: "Partners",
                        principalColumn: "partnerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userRef = table.Column<long>(type: "bigint", nullable: true),
                    roleID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_roleID",
                        column: x => x.roleID,
                        principalTable: "Roles",
                        principalColumn: "roleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Users_userRef",
                        column: x => x.userRef,
                        principalTable: "Users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPartners",
                columns: table => new
                {
                    userPartID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<long>(type: "bigint", nullable: false),
                    partnerID = table.Column<long>(type: "bigint", nullable: false),
                    balanceID = table.Column<long>(type: "bigint", nullable: false),
                    commissionID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPartners", x => x.userPartID);
                    table.ForeignKey(
                        name: "FK_UserPartners_AccountBalances_balanceID",
                        column: x => x.balanceID,
                        principalTable: "AccountBalances",
                        principalColumn: "balanceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPartners_Commissions_commissionID",
                        column: x => x.commissionID,
                        principalTable: "Commissions",
                        principalColumn: "commissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPartners_Partners_partnerID",
                        column: x => x.partnerID,
                        principalTable: "Partners",
                        principalColumn: "partnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPartners_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_userID",
                table: "RefreshToken",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_ServeProds_partnerspartnerID",
                table: "ServeProds",
                column: "partnerspartnerID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPartners_balanceID",
                table: "UserPartners",
                column: "balanceID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPartners_commissionID",
                table: "UserPartners",
                column: "commissionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPartners_partnerID",
                table: "UserPartners",
                column: "partnerID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPartners_userID",
                table: "UserPartners",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_roleID",
                table: "Users",
                column: "roleID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_userRef",
                table: "Users",
                column: "userRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "ServeProds");

            migrationBuilder.DropTable(
                name: "UserPartners");

            migrationBuilder.DropTable(
                name: "AccountBalances");

            migrationBuilder.DropTable(
                name: "Commissions");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
