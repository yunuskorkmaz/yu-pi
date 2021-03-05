using Microsoft.EntityFrameworkCore.Migrations;

namespace yu_pi.Migrations
{
    public partial class tunnelEntitiyRevize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicUrl",
                table: "Tunnels",
                newName: "publicUrl");

            migrationBuilder.RenameColumn(
                name: "Proto",
                table: "Tunnels",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "LocalAddress",
                table: "Tunnels",
                newName: "Protokol");

            migrationBuilder.AddColumn<string>(
                name: "Port",
                table: "Tunnels",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Port",
                table: "Tunnels");

            migrationBuilder.RenameColumn(
                name: "publicUrl",
                table: "Tunnels",
                newName: "PublicUrl");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Tunnels",
                newName: "Proto");

            migrationBuilder.RenameColumn(
                name: "Protokol",
                table: "Tunnels",
                newName: "LocalAddress");
        }
    }
}
