using Microsoft.EntityFrameworkCore;
using Ironfrost.Authentication.Services.Entities;

namespace Ironfrost.Authentication.Services;

public interface IDataBaseService
{
    DbSet<Users> Users();
    
    DbContext DbContext { get; }
    void SaveChanges();
    Task<int> SaveChangesAsync();
}