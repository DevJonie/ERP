using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.ProductCatalog.Persistence.Migrations
{
    public partial class initmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ProductCatalog");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "ProductCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float(4)", precision: 4, nullable: false, defaultValue: 0.0),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "USD")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products",
                schema: "ProductCatalog");
        }
    }
}
