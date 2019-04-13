using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GatewayService.Migrations
{
    public partial class FullDbMigrationVersion4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AhphConfigReRoutes");

            migrationBuilder.DropTable(
                name: "AhphGlobalConfiguration");

            migrationBuilder.DropTable(
                name: "AhphReRoute");

            migrationBuilder.DropTable(
                name: "AhphReRoutesItem");

            migrationBuilder.CreateTable(
                name: "GlobalConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GatewayName = table.Column<string>(maxLength: 100, nullable: false),
                    RequestIdKey = table.Column<string>(maxLength: 100, nullable: true),
                    BaseUrl = table.Column<string>(maxLength: 100, nullable: true),
                    DownstreamScheme = table.Column<string>(maxLength: 50, nullable: true),
                    ServiceDiscoveryProvider = table.Column<string>(maxLength: 500, nullable: true),
                    LoadBalancerOptions = table.Column<string>(maxLength: 500, nullable: true),
                    HttpHandlerOptions = table.Column<string>(maxLength: 500, nullable: true),
                    QoSOptions = table.Column<string>(maxLength: 200, nullable: true),
                    IsDefault = table.Column<int>(nullable: false),
                    InfoStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReRoutes",
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
                    InfoStatus = table.Column<int>(nullable: false),
                    Memo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReRoutes", x => x.ReRouteId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalConfigurations");

            migrationBuilder.DropTable(
                name: "ReRoutes");

            migrationBuilder.CreateTable(
                name: "AhphConfigReRoutes",
                columns: table => new
                {
                    CtgRouteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AhphId = table.Column<int>(nullable: true),
                    ReRouteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AhphConfigReRoutes", x => x.CtgRouteId);
                });

            migrationBuilder.CreateTable(
                name: "AhphGlobalConfiguration",
                columns: table => new
                {
                    AhphId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseUrl = table.Column<string>(maxLength: 100, nullable: true),
                    DownstreamScheme = table.Column<string>(maxLength: 50, nullable: true),
                    GatewayName = table.Column<string>(maxLength: 100, nullable: false),
                    HttpHandlerOptions = table.Column<string>(maxLength: 500, nullable: true),
                    InfoStatus = table.Column<int>(nullable: false),
                    IsDefault = table.Column<int>(nullable: false),
                    LoadBalancerOptions = table.Column<string>(maxLength: 500, nullable: true),
                    QoSOptions = table.Column<string>(maxLength: 200, nullable: true),
                    RequestIdKey = table.Column<string>(maxLength: 100, nullable: true),
                    ServiceDiscoveryProvider = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AhphGlobalConfiguration", x => x.AhphId);
                });

            migrationBuilder.CreateTable(
                name: "AhphReRoute",
                columns: table => new
                {
                    ReRouteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthenticationOptions = table.Column<string>(maxLength: 300, nullable: true),
                    CacheOptions = table.Column<string>(maxLength: 200, nullable: true),
                    DelegatingHandlers = table.Column<string>(maxLength: 200, nullable: true),
                    DownstreamHostAndPorts = table.Column<string>(maxLength: 500, nullable: true),
                    DownstreamPathTemplate = table.Column<string>(maxLength: 200, nullable: false),
                    DownstreamScheme = table.Column<string>(maxLength: 50, nullable: false),
                    InfoStatus = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: true),
                    LoadBalancerOptions = table.Column<string>(maxLength: 500, nullable: true),
                    Priority = table.Column<int>(nullable: true),
                    QoSOptions = table.Column<string>(maxLength: 200, nullable: true),
                    RequestIdKey = table.Column<string>(maxLength: 100, nullable: true),
                    ServiceName = table.Column<string>(maxLength: 100, nullable: true),
                    UpstreamHost = table.Column<string>(maxLength: 100, nullable: true),
                    UpstreamHttpMethod = table.Column<string>(maxLength: 50, nullable: false),
                    UpstreamPathTemplate = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AhphReRoute", x => x.ReRouteId);
                });

            migrationBuilder.CreateTable(
                name: "AhphReRoutesItem",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InfoStatus = table.Column<int>(nullable: false),
                    ItemDetail = table.Column<string>(maxLength: 500, nullable: true),
                    ItemName = table.Column<string>(maxLength: 100, nullable: false),
                    ItemParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AhphReRoutesItem", x => x.ItemId);
                });
        }
    }
}
