using Microsoft.EntityFrameworkCore.Migrations;

namespace GatewayService.Migrations
{
    public partial class UpdateReRouteMemoField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "GatewayServer",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "GatewayServer",
                newName: "ReRoutes");

            migrationBuilder.AddColumn<string>(
                name: "Memo",
                table: "ReRoutes",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReRoutes",
                table: "ReRoutes",
                column: "ReRouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReRoutes",
                table: "ReRoutes");

            migrationBuilder.DropColumn(
                name: "Memo",
                table: "ReRoutes");

            migrationBuilder.EnsureSchema(
                name: "GatewayServer");

            migrationBuilder.RenameTable(
                name: "ReRoutes",
                newName: "User",
                newSchema: "GatewayServer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "GatewayServer",
                table: "User",
                column: "ReRouteId");
        }
    }
}
