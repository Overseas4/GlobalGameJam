public enum ItemType
{
    Wood,
    Water,
    SeaWeed,
    Sand,
    SeaShell,
    SuperWood
}

public interface IIventoryItem 
{
    ItemType Type { get; set; }
    float RepairValue { get; set; }
    float Weight { get; set; }
}
