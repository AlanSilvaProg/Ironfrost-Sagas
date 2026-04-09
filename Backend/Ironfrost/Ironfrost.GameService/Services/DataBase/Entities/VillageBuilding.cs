using System.ComponentModel.DataAnnotations.Schema;

namespace Ironfrost.GameService.Services.DataBase.Entities;

[Table("village_buildings")]
public class VillageBuilding
{
    public Guid village_id { get; set; }
    public BuildingType building_type { get; set; }
    public byte level { get; set; }
    public bool is_upgrading { get; set; }
    public DateTime? upgrade_finish_at { get; set; }
}

public enum BuildingType : byte
{
    Headquarters = 0,
    Barracks = 1,
    Stable = 2,
    Workshop = 3,
    Smithy = 4,
    Market = 5,
    TimberCamp = 6,
    ClayPit = 7,
    IronMine = 8,
    Farm = 9,
    Warehouse = 10,
    Wall = 11,
    RallyPoint = 12,
    Church = 13,
    Academy = 14
}
