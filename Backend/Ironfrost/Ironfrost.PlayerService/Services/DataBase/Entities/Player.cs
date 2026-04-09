using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ironfrost.PlayerService.Services.DataBase.Entities;

[Table("players")]
public class Player
{
    [Required] [Key] public Guid id { get; set; }
    public Guid? world_id { get; set; }
    public string username { get; set; }
    public Guid? aett_id { get; set; }
    public byte emblem_index { get; set; }
    public uint total_points { get; set; }
    public ushort village_count { get; set; }
    public DateTime created_at { get; set; }
}
