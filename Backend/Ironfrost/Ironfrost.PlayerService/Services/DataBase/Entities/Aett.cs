using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ironfrost.PlayerService.Services.DataBase.Entities;

[Table("aetts")]
public class Aett
{
    [Required] [Key] public Guid id { get; set; }
    public Guid world_id { get; set; }
    public string aett_name { get; set; }
    public Guid leader_id { get; set; }
    public byte emblem_index { get; set; }
    public ushort member_count { get; set; }
    public uint total_points { get; set; }
    public DateTime created_at { get; set; }
}
