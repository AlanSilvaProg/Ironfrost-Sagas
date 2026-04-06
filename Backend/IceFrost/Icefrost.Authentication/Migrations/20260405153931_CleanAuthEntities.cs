using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Icefrost.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class CleanAuthEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "aett_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "emblem_index",
                table: "users");

            migrationBuilder.DropColumn(
                name: "total_points",
                table: "users");

            migrationBuilder.DropColumn(
                name: "village_count",
                table: "users");

            migrationBuilder.DropColumn(
                name: "world_id",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "aett_id",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "emblem_index",
                table: "users",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<long>(
                name: "total_points",
                table: "users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "village_count",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "world_id",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "aetts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    aett_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    emblem_index = table.Column<byte>(type: "smallint", nullable: false),
                    leader_id = table.Column<Guid>(type: "uuid", nullable: false),
                    member_count = table.Column<int>(type: "integer", nullable: false),
                    total_points = table.Column<long>(type: "bigint", nullable: false),
                    world_id = table.Column<Guid>(type: "uuid", nullable: false)
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
                    is_upgrading = table.Column<bool>(type: "boolean", nullable: false),
                    level = table.Column<byte>(type: "smallint", nullable: false),
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
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    loyalty = table.Column<byte>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: true),
                    points = table.Column<long>(type: "bigint", nullable: false),
                    world_id = table.Column<Guid>(type: "uuid", nullable: false),
                    x = table.Column<short>(type: "smallint", nullable: false),
                    y = table.Column<short>(type: "smallint", nullable: false)
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
                    bot_count = table.Column<int>(type: "integer", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    server_type = table.Column<byte>(type: "smallint", nullable: false),
                    speed_modifier = table.Column<float>(type: "real", nullable: false),
                    wild_villages = table.Column<int>(type: "integer", nullable: false),
                    win_condition = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worlds", x => x.id);
                });
        }
    }
}
