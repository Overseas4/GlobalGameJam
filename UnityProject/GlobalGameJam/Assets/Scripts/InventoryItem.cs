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
			for (int i = 0; i < Inventory.Instance.Items.Count; i++)
			{
				if (Inventory.Instance.Items[i].type == ItemType.Sand)
				{
                    eventController.Instance.ACT_eau_pickUp.Post(gameObject);
                    UIInventory.Instance.Notify();
                    InventoryStruct wetWater = new InventoryStruct(ItemType.WetSand,5f,5f);
                    Inventory.Instance.AddItem(new InventoryStruct(Type,Weight,RepairValue));
				}
			}
		}
        else if (Type == ItemType.Sand)
        {
            bool canPickUpSand = true;
            foreach (InventoryStruct item in Inventory.Instance.Items)
            {
                if (item.type == ItemType.Sand || item.type == ItemType.WetSand)
                {
                    canPickUpSand = false;
                    break;
                }
            }
            if (canPickUpSand)
            {
                eventController.Instance.ACT_sable_pickUp.Post(gameObject);
                UIInventory.Instance.Notify();
                Inventory.Instance.AddItem(new InventoryStruct(Type, Weight, RepairValue));
            }
        }
		else
		{
            switch(Type)
            {
                case ItemType.SeaShell:
                    break;
                case ItemType.SeaWeed:
                    eventController.Instance.ACT_algue_pickUp.Post(gameObject);
                    break;
                case ItemType.SuperWood:
                    eventController.Instance.ACT_superBois_pickUp.Post(gameObject);
                    break;
                case ItemType.Wood:
                    eventController.Instance.ACT_bois_pickUp.Post(gameObject);
                    break;
            }
			Inventory.Instance.AddItem(new InventoryStruct(Type, Weight, RepairValue));
			UIInventory.Instance.Notify();
			Destroy(gameObject);
		}
	}
}

public struct InventoryStruct
{
	public ItemType type;
	public float weight;
	public float repairValue;

	public InventoryStruct(ItemType type, float weight, float repairValue)
	{
		this.type = type;
		this.weight = weight;
		this.repairValue = repairValue;
	}
}
