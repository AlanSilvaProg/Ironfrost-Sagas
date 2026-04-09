using Microsoft.EntityFrameworkCore;
using Ironfrost.Authentication.Services.Entities;

namespace Ironfrost.Authentication.Services;

public class PostgreDbService(IConfiguration configuration) : DataBaseService
{ 
    private readonly ProjectDb _dbContext = new PostgreDb(configuration);
    
    public override DbSet<Users> Users() => _dbContext.Users;

    protected override DbContext GetDbContext() => _dbContext;
}

public class PostgreDb(IConfiguration configuration) : ProjectDb
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql(@$"Host={configuration["DbConnection:Host"]};Port={configuration["DbConnection:Port"]};Username={configuration["DbConnection:Username"]};Password={configuration["DbConnection:Password"]};Database={configuration["DbConnection:Database"]};SSL Mode=Require;Trust Server Certificate=true");
}