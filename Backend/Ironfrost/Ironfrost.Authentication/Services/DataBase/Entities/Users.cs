using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ironfrost.Authentication.Services.Entities;

[Table("users")]
public class Users
{
    [Required] [Key] public Guid id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public bool is_admin { get; set; }
    public DateTime created_at { get; set; }

    public Users(){}

    public Users(Guid id, string username = "", string email = "", string password = "", bool is_admin = false,
        DateTime? created_at = null)
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.password = password;
        this.is_admin = is_admin;
        this.created_at = created_at ?? DateTime.UtcNow;
    }
}
