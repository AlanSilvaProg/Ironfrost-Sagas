using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Icefrost.GameService.Services.DataBase.Entities;

[Table("villages")]
public class Village
{
    [Required] [Key] public Guid id { get; set; }
    public Guid world_id { get; set; }
    public Guid? owner_id { get; set; }
    public string name { get; set; }
    public short x { get; set; }
    public short y { get; set; }
    public uint points { get; set; }
    public byte loyalty { get; set; }
    public DateTime last_updated_at { get; set; }
    public DateTime created_at { get; set; }
}
