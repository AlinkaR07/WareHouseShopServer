using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Number = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataOrder = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataShipment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOrganizationPostavshik_FK_ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIOworker_FK_ = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "Tovar",
                columns: table => new
                {
                    CodTovara = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tovar", x => x.CodTovara);
                });

            migrationBuilder.CreateTable(
                name: "Write",
                columns: table => new
                {
                    NumberAct = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataWrite = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FIOworker_FK_ = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Write", x => x.NumberAct);
                });

            migrationBuilder.CreateTable(
                name: "LineOrder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasePrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountOrder = table.Column<int>(type: "int", nullable: false),
                    CountShipment = table.Column<int>(type: "int", nullable: true),
                    CodTovara_FK_ = table.Column<int>(type: "int", nullable: false),
                    NumberOrder_FK_ = table.Column<int>(type: "int", nullable: false),
                    DataManuf = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TovarCodTovara = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineOrder", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineOrder_Order_NumberOrder_FK_",
                        column: x => x.NumberOrder_FK_,
                        principalTable: "Order",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineOrder_Tovar_TovarCodTovara",
                        column: x => x.TovarCodTovara,
                        principalTable: "Tovar",
                        principalColumn: "CodTovara",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineWrite",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summa = table.Column<double>(type: "float", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    NumberActWrite_FK_ = table.Column<int>(type: "int", nullable: false),
                    CodTovara_FK_ = table.Column<int>(type: "int", nullable: false),
                    TovarCodTovara = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineWrite", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineWrite_Tovar_TovarCodTovara",
                        column: x => x.TovarCodTovara,
                        principalTable: "Tovar",
                        principalColumn: "CodTovara",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineWrite_Write_NumberActWrite_FK_",
                        column: x => x.NumberActWrite_FK_,
                        principalTable: "Write",
                        principalColumn: "NumberAct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineOrder_NumberOrder_FK_",
                table: "LineOrder",
                column: "NumberOrder_FK_");

            migrationBuilder.CreateIndex(
                name: "IX_LineOrder_TovarCodTovara",
                table: "LineOrder",
                column: "TovarCodTovara");

            migrationBuilder.CreateIndex(
                name: "IX_LineWrite_NumberActWrite_FK_",
                table: "LineWrite",
                column: "NumberActWrite_FK_");

            migrationBuilder.CreateIndex(
                name: "IX_LineWrite_TovarCodTovara",
                table: "LineWrite",
                column: "TovarCodTovara");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineOrder");

            migrationBuilder.DropTable(
                name: "LineWrite");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Tovar");

            migrationBuilder.DropTable(
                name: "Write");
        }
    }
}
