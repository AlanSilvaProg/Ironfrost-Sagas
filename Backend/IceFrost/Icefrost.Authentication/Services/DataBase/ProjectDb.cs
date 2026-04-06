using Microsoft.EntityFrameworkCore;
using Icefrost.Authentication.Services.Entities;

namespace Icefrost.Authentication.Services;

public class ProjectDb() : DbContext
{
    public DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().HasKey(u => u.id);
        base.OnModelCreating(modelBuilder);
    }
}