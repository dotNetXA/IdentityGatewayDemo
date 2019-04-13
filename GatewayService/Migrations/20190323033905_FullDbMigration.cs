using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GatewayService.Migrations
{
    public partial class FullDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalConfigurations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
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
                    table.PrimaryKey("PK_GlobalConfigurations", x => x.Id);
                });
        }
    }
}
