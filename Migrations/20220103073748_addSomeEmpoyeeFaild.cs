using Microsoft.EntityFrameworkCore.Migrations;

namespace kudvenkit.Migrations
{
    public partial class addSomeEmpoyeeFaild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "employees");
        }
    }
}
