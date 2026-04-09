using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ironfrost.GameService.Services.DataBase.Entities;

[Table("worlds")]
public class World
{
    [Required] [Key] public Guid id { get; set; }
    public ushort capacity { get; set; }
    public ushort bot_count { get; set; }
    public ushort wild_villages { get; set; }
    public byte server_type { get; set; }
    public byte win_condition { get; set; }
    public float speed_modifier { get; set; }
    public DateTime created_at { get; set; }
}
