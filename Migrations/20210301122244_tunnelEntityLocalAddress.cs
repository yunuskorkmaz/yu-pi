using Microsoft.EntityFrameworkCore.Migrations;

namespace yu_pi.Migrations
{
    public partial class tunnelEntityLocalAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalAddress",
                table: "Tunnels",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalAddress",
                table: "Tunnels");
        }
    }
}
