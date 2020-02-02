using UnityEngine;

public class InventoryItem : Interactible, IIventoryItem
{
    [SerializeField] private ItemType _type = ItemType.Wood;
    [SerializeField] private float _weight = 0f;
    [SerializeField] private float _repairValue = 0f;

    public ItemType Type { get => _type; set => _type = value; }
    public float RepairValue { get => _repairValue; set => _repairValue = value; }
    public float Weight { get => _weight; set => _weight = value; }

    public override void Interact()
    {
        if (Type == ItemType.Water)
        {
            bool hasSand = false;
            for(int i = 0; i < Inventory.Instance.Items.Count; i++)
            {
                if(Inventory.Instance.Items[i].Type == ItemType.Sand)
                {
                    hasSand = true;
                    break;
                }
            }
        }
        UIInventory.Instance.Notify();
        Inventory.Instance.AddItem(this);
    }
}
