using Microsoft.EntityFrameworkCore;
using Ironfrost.PlayerService.Services.DataBase.Entities;

namespace Ironfrost.PlayerService.Services.DataBase;

public class PlayerDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Aett> Aetts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            @$"Host={configuration["DbConnection:Host"]};Port={configuration["DbConnection:Port"]};Username={configuration["DbConnection:Username"]};Password={configuration["DbConnection:Password"]};Database={configuration["DbConnection:Database"]}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>().HasKey(p => p.id);
        modelBuilder.Entity<Aett>().HasKey(a => a.id);

        base.OnModelCreating(modelBuilder);
    }
}
