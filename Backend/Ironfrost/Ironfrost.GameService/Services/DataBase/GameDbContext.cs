using Microsoft.EntityFrameworkCore;
using Ironfrost.GameService.Services.DataBase.Entities;

namespace Ironfrost.GameService.Services.DataBase;

public class GameDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<World> Worlds { get; set; }
    public DbSet<Village> Villages { get; set; }
    public DbSet<VillageBuilding> VillageBuildings { get; set; }
    public DbSet<VillageTroop> VillageTroops { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            @$"Host={configuration["DbConnection:Host"]};Port={configuration["DbConnection:Port"]};Username={configuration["DbConnection:Username"]};Password={configuration["DbConnection:Password"]};Database={configuration["DbConnection:Database"]};SSL Mode=Require;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<World>().HasKey(w => w.id);
        modelBuilder.Entity<Village>().HasKey(v => v.id);

        modelBuilder.Entity<VillageBuilding>()
            .HasKey(vb => new { vb.village_id, vb.building_type });

        modelBuilder.Entity<VillageTroop>()
            .HasKey(vt => new { vt.village_id, vt.troop_type });

        base.OnModelCreating(modelBuilder);
    }
}
