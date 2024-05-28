using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectApi.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Material = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsItPure = table.Column<bool>(type: "bit", nullable: false),
                    InStock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SocialSctyNum = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    VipAccount = table.Column<bool>(type: "bit", nullable: false),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ForAGift = table.Column<bool>(type: "bit", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    WantCustomDesign = table.Column<bool>(type: "bit", nullable: false),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Customer_ID = table.Column<int>(type: "int", nullable: false),
                    Currency_ID = table.Column<int>(type: "int", nullable: false),
                    Baker_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Currency_Baker_ID",
                        column: x => x.Baker_ID,
                        principalTable: "Currency",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Customers_Customer_ID",
                        column: x => x.Customer_ID,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Baker_ID",
                table: "Orders",
                column: "Baker_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Customer_ID",
                table: "Orders",
                column: "Customer_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
