using System.ComponentModel.DataAnnotations.Schema;

namespace Icefrost.GameService.Services.DataBase.Entities;

[Table("village_troops")]
public class VillageTroop
{
    public Guid village_id { get; set; }
    public TroopType troop_type { get; set; }
    public ushort quantity { get; set; }
}

public enum TroopType : byte
{
    Spearman = 0,
    Swordsman = 1,
    Axeman = 2,
    Archer = 3,
    LightCavalry = 4,
    HeavyCavalry = 5,
    MountedArcher = 6,
    Ram = 7,
    Catapult = 8,
    Noble = 9,
    Scout = 10
}
