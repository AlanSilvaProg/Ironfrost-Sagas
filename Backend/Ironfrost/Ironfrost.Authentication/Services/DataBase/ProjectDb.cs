using Microsoft.EntityFrameworkCore;
using Ironfrost.Authentication.Services.Entities;

namespace Ironfrost.Authentication.Services;

public class ProjectDb() : DbContext
{
    public DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().HasKey(u => u.id);
        base.OnModelCreating(modelBuilder);
    }
}