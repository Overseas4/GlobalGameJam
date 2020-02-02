using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
	[SerializeField] private GameObject sand;
	[SerializeField] private GameObject wood;
	[SerializeField] private GameObject wetSand;
	[SerializeField] private GameObject superWood;

	[SerializeField] private GameObject sandHighlight;
	[SerializeField] private GameObject woodHighlight;
	[SerializeField] private GameObject wetSandHighlight;
	[SerializeField] private GameObject superWoodHighlight;

	[SerializeField] private Image sandNumberIcon;
	[SerializeField] private Image woodNumberIcon;
	[SerializeField] private Image wetSandNumberIcon;
	[SerializeField] private Image superWoodNumberIcon;

	[SerializeField] private Sprite oneTexture;
	[SerializeField] private Sprite twoTexture;
	[SerializeField] private Sprite threeTexture;
	[SerializeField] private Sprite fourTexture;
	[SerializeField] private Sprite fiveTexture;

	public static UIInventory Instance;
	private GameObject selectedHighlight;
	private void Awake()
	{
		Instance = this;
		sand.SetActive(false);
		wetSand.SetActive(false);
		wood.SetActive(false);
		superWood.SetActive(false);

	}

	private void Start()
	{
		//Notify();
	}

	public void Notify()
	{
		int numberOfWood = 0;
		int numberOfSuperWood = 0;
		int numberOfWetSand = 0;
		int numberOfSand = 0;

		foreach (var items in Inventory.Instance.Items)
		{
			switch (items.type)
			{
				case ItemType.Wood:
					++numberOfWood;
					break;
				case ItemType.Sand:
					++numberOfSand;
					break;
				case ItemType.SuperWood:
					++numberOfSuperWood;
					break;
				case ItemType.WetSand:
					++numberOfWetSand;
					break;
				default:
					break;
			}
		}

		GameObject[] gameObjects = new GameObject[] { wood, superWood, wetSand, sand };
		GameObject[] highlight = new GameObject[] { woodHighlight, superWoodHighlight, wetSandHighlight, sandHighlight };
		Image[] icons = new Image[] { woodNumberIcon, superWoodNumberIcon, wetSandNumberIcon, sandNumberIcon };
		int[] numbers = new int[] { numberOfWood, numberOfSuperWood, numberOfWetSand, numberOfSand };

		for (int i = 0; i < gameObjects.Length; i++)
		{
			if (numbers[i] == 0)
			{
				gameObjects[i].SetActive(false);
				if (selectedHighlight == highlight[i])
				{
					SelectNewHighlight();
				}
			}
			else
			{
				gameObjects[i].SetActive(true);

				if (selectedHighlight == null)
				{
					selectedHighlight = highlight[i];
					selectedHighlight.SetActive(true);
				}
			}

			switch (numbers[i])
			{
				case 1:
					icons[i].sprite = oneTexture;
					break;
				case 2:
					icons[i].sprite = twoTexture;
					break;
				case 3:
					icons[i].sprite = threeTexture;
					break;
				case 4:
					icons[i].sprite = fourTexture;
					break;
				case 5:
					icons[i].sprite = fiveTexture;
					break;
				default:
					break;
			}
		}
	}

	private void SelectNewHighlight()
	{
		if (selectedHighlight != null)
		{
			selectedHighlight.SetActive(false);
		}

		int numberOfWood = 0;
		int numberOfSuperWood = 0;
		int numberOfWetSand = 0;
		int numberOfSand = 0;

		foreach (var items in Inventory.Instance.Items)
		{
			switch (items.type)
			{
				case ItemType.Wood:
					++numberOfWood;
					break;
				case ItemType.Sand:
					++numberOfSand;
					break;
				case ItemType.SuperWood:
					++numberOfSuperWood;
					break;
				case ItemType.WetSand:
					++numberOfWetSand;
					break;
				default:
					break;
			}
		}

		if (numberOfSand != 0)
		{
			selectedHighlight = sandHighlight;
			selectedHighlight.SetActive(true);
		}
		if (numberOfWetSand != 0)
		{
			selectedHighlight = wetSandHighlight;
			selectedHighlight.SetActive(true);
		}
		if (numberOfSuperWood != 0)
		{
			selectedHighlight = superWoodHighlight;
			selectedHighlight.SetActive(true);
		}
		if (numberOfWood != 0)
		{
			selectedHighlight = woodHighlight;
			selectedHighlight.SetActive(true);
		}
	}

	public InventoryContainer GetCurrentSelected()
	{
		ItemType type;
		if (selectedHighlight == sandHighlight)
		{
			type = ItemType.Sand;
			for (int i = 0; i < Inventory.Instance.Items.Count; i++)
			{
				if (Inventory.Instance.Items[i].type == type)
				{
					return Inventory.Instance.Items[i];
				}
			}
		}
		if (selectedHighlight == woodHighlight)
		{
			type = ItemType.Wood;
			for (int i = 0; i < Inventory.Instance.Items.Count; i++)
			{
				if (Inventory.Instance.Items[i].type == type)
				{
					return Inventory.Instance.Items[i];
				}
			}
		}
		if (selectedHighlight == wetSandHighlight)
		{
			type = ItemType.WetSand;
			for (int i = 0; i < Inventory.Instance.Items.Count; i++)
			{
				if (Inventory.Instance.Items[i].type == type)
				{
					return Inventory.Instance.Items[i];
				}
			}
		}
		if (selectedHighlight == superWoodHighlight)
		{
			type = ItemType.SuperWood;
			for (int i = 0; i < Inventory.Instance.Items.Count; i++)
			{
				if (Inventory.Instance.Items[i].type == type)
				{
					return Inventory.Instance.Items[i];
				}
			}
		}
		return null;
	}

	internal void RemoveInstance(InventoryContainer itemToRemove)
	{
		Inventory.Instance.RemoveItem(itemToRemove);
		Notify();
	}

	void MoveHighlightUp()
	{
		List<GameObject> highlights = new List<GameObject>();
		int numberOfWood = 0;
		int numberOfSuperWood = 0;
		int numberOfWetSand = 0;
		int numberOfSand = 0;

		foreach (var items in Inventory.Instance.Items)
		{
			switch (items.type)
			{
				case ItemType.Wood:
					++numberOfWood;
					break;
				case ItemType.Sand:
					++numberOfSand;
					break;
				case ItemType.SuperWood:
					++numberOfSuperWood;
					break;
				case ItemType.WetSand:
					++numberOfWetSand;
					break;
				default:
					break;
			}
		}

		if (numberOfWood != 0)
		{
			highlights.Add(sandHighlight);
		}
		if (numberOfSuperWood != 0)
		{
			highlights.Add(wetSandHighlight);
		}
		if (numberOfSand != 0)
		{
			highlights.Add(superWoodHighlight);
		}
		if (numberOfWetSand != 0)
		{
			highlights.Add(woodHighlight);
		}

		if (highlights.Count != 0)
		{
			for (int i = 0; i < highlights.Count; i++)
			{
				if (selectedHighlight == highlights[i])
				{
					selectedHighlight.SetActive(false);
					if (i - 1 >= 0)
					{
						selectedHighlight = highlights[i - 1];
						selectedHighlight.SetActive(true);
					}
					else
					{
						selectedHighlight = highlights[highlights.Count - 1];
						selectedHighlight.SetActive(true);
					}
					break;
				}
			}
		}
		else
		{
			if (selectedHighlight != null)
			{
				selectedHighlight.SetActive(false);
			}
			selectedHighlight = null;
		}
	}

	void MoveHighlightDown()
	{
		List<GameObject> highlights = new List<GameObject>();
		int numberOfWood = 0;
		int numberOfSuperWood = 0;
		int numberOfWetSand = 0;
		int numberOfSand = 0;

		foreach (var items in Inventory.Instance.Items)
		{
			switch (items.type)
			{
				case ItemType.Wood:
					++numberOfWood;
					break;
				case ItemType.Sand:
					++numberOfSand;
					break;
				case ItemType.SuperWood:
					++numberOfSuperWood;
					break;
				case ItemType.WetSand:
					++numberOfWetSand;
					break;
				default:
					break;
			}
		}

		if (numberOfWood != 0)
		{
			highlights.Add(sandHighlight);
		}
		if (numberOfSuperWood != 0)
		{
			highlights.Add(wetSandHighlight);
		}
		if (numberOfSand != 0)
		{
			highlights.Add(superWoodHighlight);
		}
		if (numberOfWetSand != 0)
		{
			highlights.Add(woodHighlight);
		}

		if (highlights.Count != 0)
		{
			for (int i = 0; i < highlights.Count; i++)
			{
				if (selectedHighlight == highlights[i])
				{
					selectedHighlight.SetActive(false);
					if (i + 1 < highlights.Count)
					{
						selectedHighlight = highlights[i + 1];
						selectedHighlight.SetActive(true);
					}
					else
					{
						selectedHighlight = highlights[0];
						selectedHighlight.SetActive(true);
					}
					break;
				}
			}
		}
		else
		{
			if (selectedHighlight != null)
			{
				selectedHighlight.SetActive(false);
			}
			selectedHighlight = null;
		}
	}
}
