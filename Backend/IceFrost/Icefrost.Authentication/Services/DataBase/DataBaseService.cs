using Microsoft.EntityFrameworkCore;
using Icefrost.Authentication.Services.Entities;

namespace Icefrost.Authentication.Services;

public abstract class DataBaseService : IDataBaseService
{
    public DbContext DbContext => GetDbContext();
    
    public void SaveChanges() => DbContext.SaveChanges();
    public Task<int> SaveChangesAsync() => DbContext.SaveChangesAsync();

    public abstract DbSet<Users> Users();
    protected abstract DbContext GetDbContext();
}