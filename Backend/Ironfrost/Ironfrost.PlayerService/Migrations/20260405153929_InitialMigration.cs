using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ironfrost.PlayerService.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                name: "players",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    world_id = table.Column<Guid>(type: "uuid", nullable: true),
                    username = table.Column<string>(type: "text", nullable: false),
                    aett_id = table.Column<Guid>(type: "uuid", nullable: true),
                    emblem_index = table.Column<byte>(type: "smallint", nullable: false),
                    total_points = table.Column<long>(type: "bigint", nullable: false),
                    village_count = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aetts");

            migrationBuilder.DropTable(
                name: "players");
        }
    }
}
