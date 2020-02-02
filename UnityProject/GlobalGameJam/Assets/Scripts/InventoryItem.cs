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
					InventoryContainer wetSand = new InventoryContainer(ItemType.WetSand, 5f, 5f);

					var sand = Inventory.Instance.Items[i];
					Inventory.Instance.RemoveItem(sand);

					if (!Inventory.Instance.IsFull)
					{
						Inventory.Instance.AddItem(wetSand);
						UIInventory.Instance.Notify();
					}
					else
					{
						Inventory.Instance.AddItem(sand);
						UIInventory.Instance.Notify();
					}
				}
			}
		}
		else if (Type == ItemType.Sand)
		{
			bool canPickUpSand = true;
			foreach (InventoryContainer item in Inventory.Instance.Items)
			{
				if (item.type == ItemType.Sand || item.type == ItemType.WetSand)
				{
					canPickUpSand = false;
					break;
				}
			}
			if (canPickUpSand)
			{
				if (!Inventory.Instance.IsFull)
				{
					eventController.Instance.ACT_sable_pickUp.Post(gameObject);
					Inventory.Instance.AddItem(new InventoryContainer(Type, Weight, RepairValue));
					UIInventory.Instance.Notify();
				}
			}
		}
		else
		{
			switch (Type)
			{
				case ItemType.SeaShell:
					break;
				case ItemType.SeaWeed:
					eventController.Instance.ACT_algue_pickUp.Post(eventController.Instance.gameObject);
					break;
				case ItemType.SuperWood:
					eventController.Instance.ACT_superBois_pickUp.Post(eventController.Instance.gameObject);
					break;
				case ItemType.Wood:
					eventController.Instance.ACT_bois_pickUp.Post(eventController.Instance.gameObject);
					break;
			}
			if (!Inventory.Instance.IsFull)
			{
				Inventory.Instance.AddItem(new InventoryContainer(Type, Weight, RepairValue));
				UIInventory.Instance.Notify();
				Destroy(gameObject);
			}
		}
	}
}

public class InventoryContainer
{
	public ItemType type;
	public float weight;
	public float repairValue;

	public InventoryContainer(ItemType type, float weight, float repairValue)
	{
		this.type = type;
		this.weight = weight;
		this.repairValue = repairValue;
	}
}
