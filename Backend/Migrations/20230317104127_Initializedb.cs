using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carwale.Migrations
{
    /// <inheritdoc />
    public partial class Initializedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cw_make",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UId = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cw_make", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cw_tenant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UId = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cw_tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cw_model",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MakeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UId = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cw_model", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cw_model_cw_make_MakeId",
                        column: x => x.MakeId,
                        principalTable: "cw_make",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cw_user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UId = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cw_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cw_user_cw_tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "cw_tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cw_car",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    ModelId = table.Column<int>(type: "INTEGER", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Colour = table.Column<string>(type: "TEXT", nullable: true),
                    CurrentValue = table.Column<double>(type: "REAL", nullable: false),
                    Status = table.Column<byte>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UId = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cw_car", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cw_car_cw_model_ModelId",
                        column: x => x.ModelId,
                        principalTable: "cw_model",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cw_car_cw_tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "cw_tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cw_car_ModelId",
                table: "cw_car",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_cw_car_TenantId",
                table: "cw_car",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_cw_car_UId",
                table: "cw_car",
                column: "UId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cw_make_Name",
                table: "cw_make",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cw_make_UId",
                table: "cw_make",
                column: "UId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cw_model_MakeId_Name",
                table: "cw_model",
                columns: new[] { "MakeId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cw_model_UId",
                table: "cw_model",
                column: "UId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cw_tenant_UId",
                table: "cw_tenant",
                column: "UId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cw_user_TenantId",
                table: "cw_user",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_cw_user_UId",
                table: "cw_user",
                column: "UId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cw_user_UserName",
                table: "cw_user",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cw_car");

            migrationBuilder.DropTable(
                name: "cw_user");

            migrationBuilder.DropTable(
                name: "cw_model");

            migrationBuilder.DropTable(
                name: "cw_tenant");

            migrationBuilder.DropTable(
                name: "cw_make");
        }
    }
}
