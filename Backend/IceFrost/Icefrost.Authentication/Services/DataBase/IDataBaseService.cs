using Microsoft.EntityFrameworkCore;
using Icefrost.Authentication.Services.Entities;

namespace Icefrost.Authentication.Services;

public interface IDataBaseService
{
    DbSet<Users> Users();
    
    DbContext DbContext { get; }
    void SaveChanges();
    Task<int> SaveChangesAsync();
}