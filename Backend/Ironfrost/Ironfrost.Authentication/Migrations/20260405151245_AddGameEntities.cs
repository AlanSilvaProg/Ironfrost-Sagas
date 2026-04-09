using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ironfrost.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class AddGameEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aetts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    world_id = table.Column<Guid>(type: "uuid", nullable: false),
                    aett_name = table.Column<string>(type: "text", nullable: false),
                    leader_id = table.Column<Guid>(type: "uuid", nullable: false),
                    emblem_index = table.Column<byte>(type: "smallint", nullable: false),
                    member_count = table.Column<int>(type: "integer", nullable: false),
                    total_points = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aetts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "village_buildings",
                columns: table => new
                {
                    village_id = table.Column<Guid>(type: "uuid", nullable: false),
                    building_type = table.Column<byte>(type: "smallint", nullable: false),
                    level = table.Column<byte>(type: "smallint", nullable: false),
                    is_upgrading = table.Column<bool>(type: "boolean", nullable: false),
                    upgrade_finish_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_village_buildings", x => new { x.village_id, x.building_type });
                });

            migrationBuilder.CreateTable(
                name: "village_troops",
                columns: table => new
                {
                    village_id = table.Column<Guid>(type: "uuid", nullable: false),
                    troop_type = table.Column<byte>(type: "smallint", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_village_troops", x => new { x.village_id, x.troop_type });
                });

            migrationBuilder.CreateTable(
                name: "villages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    world_id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    x = table.Column<short>(type: "smallint", nullable: false),
                    y = table.Column<short>(type: "smallint", nullable: false),
                    points = table.Column<long>(type: "bigint", nullable: false),
                    loyalty = table.Column<byte>(type: "smallint", nullable: false),
                    last_updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_villages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "worlds",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    bot_count = table.Column<int>(type: "integer", nullable: false),
                    wild_villages = table.Column<int>(type: "integer", nullable: false),
                    server_type = table.Column<byte>(type: "smallint", nullable: false),
                    win_condition = table.Column<byte>(type: "smallint", nullable: false),
                    speed_modifier = table.Column<float>(type: "real", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worlds", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aetts");

            migrationBuilder.DropTable(
                name: "village_buildings");

            migrationBuilder.DropTable(
                name: "village_troops");

            migrationBuilder.DropTable(
                name: "villages");

            migrationBuilder.DropTable(
                name: "worlds");
        }
    }
}
