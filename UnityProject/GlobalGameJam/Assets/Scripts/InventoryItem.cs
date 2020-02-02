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
					UIInventory.Instance.Notify();
					Inventory.Instance.AddItem(new InventoryStruct(Type,Weight,RepairValue));
				}
			}
		}
		else
		{
			Inventory.Instance.AddItem(new InventoryStruct(Type, Weight, RepairValue));
			UIInventory.Instance.Notify();
			
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
