using Microsoft.EntityFrameworkCore.Migrations;

namespace Could_System_dev_ops.Migrations
{
    public partial class suppliername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuppilerName",
                table: "Products",
                newName: "SupplierName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierName",
                table: "Products",
                newName: "SuppilerName");
        }
    }
}
