using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GatewayService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "GatewayServer");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "GatewayServer",
                columns: table => new
                {
                    ReRouteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemId = table.Column<int>(nullable: true),
                    UpstreamPathTemplate = table.Column<string>(maxLength: 150, nullable: false),
                    UpstreamHttpMethod = table.Column<string>(maxLength: 50, nullable: false),
                    UpstreamHost = table.Column<string>(maxLength: 100, nullable: true),
                    DownstreamScheme = table.Column<string>(maxLength: 50, nullable: false),
                    DownstreamPathTemplate = table.Column<string>(maxLength: 200, nullable: false),
                    DownstreamHostAndPorts = table.Column<string>(maxLength: 500, nullable: true),
                    AuthenticationOptions = table.Column<string>(maxLength: 300, nullable: true),
                    RequestIdKey = table.Column<string>(maxLength: 100, nullable: true),
                    CacheOptions = table.Column<string>(maxLength: 200, nullable: true),
                    ServiceName = table.Column<string>(maxLength: 100, nullable: true),
                    LoadBalancerOptions = table.Column<string>(maxLength: 500, nullable: true),
                    QoSOptions = table.Column<string>(maxLength: 200, nullable: true),
                    DelegatingHandlers = table.Column<string>(maxLength: 200, nullable: true),
                    Priority = table.Column<int>(nullable: true),
                    InfoStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ReRouteId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User",
                schema: "GatewayServer");
        }
    }
}
