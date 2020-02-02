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
		sand.SetActive(numberOfSand != 0);
		wood.SetActive(numberOfWood != 0);
		superWood.SetActive(numberOfSuperWood != 0);
		GameObject[] gameObjects = new GameObject[] { wetSand, sand, wood, superWood };
		Image[] icons = new Image[] { wetSandNumberIcon, sandNumberIcon, woodNumberIcon, superWoodNumberIcon };
		int[] numbers = new int[] { numberOfWetSand, numberOfSand, numberOfWood, numberOfSuperWood };

		for (int i = 0; i < gameObjects.Length; i++)
		{
			gameObjects[i].SetActive(numbers[i] != 0);

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
}
