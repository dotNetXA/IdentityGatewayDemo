using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GatewayService.Migrations
{
    public partial class AddGlobalConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalConfigurations");
        }
    }
}
