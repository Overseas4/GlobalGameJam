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
		Notify();
	}

	public void Notify()
	{
		int numberOfSand = 0;
		int numberOfWetSand = 0;
		int numberOfWood = 0;
		int numberOfSuperWood = 0;

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

		GameObject[] gameObjects = new GameObject[] { wetSand, sand, wood, superWood };
		Image[] icons = new Image[] { wetSandNumberIcon, sandNumberIcon, woodNumberIcon, superWoodNumberIcon };
		int[] numbers = new int[] { numberOfWetSand, numberOfSand, numberOfWood, numberOfSuperWood };

		if (selectedHighlight == null)
		{

		}

		for (int i = 0; i < gameObjects.Length; i++)
		{
			if (numbers[i] == 0)
			{
				if (gameObjects[i].activeInHierarchy)
				{

				}
				else
				{

				}
				gameObjects[i].SetActive(numbers[i] != 0);
			}
			else 
			{
				if (selectedHighlight == null)
				{
					SelectThisGameObject
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

	private void SelectThisGameObject(GameObject thisONE)
	{
		selectedHighlight.SetActive(false);
		selectedHighlight = thisONE;

		if (thisONE != null)
		{
			selectedHighlight.SetActive(true);
		}
	}
}
