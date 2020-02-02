using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
	[SerializeField] private GameObject seaShell;
	[SerializeField] private GameObject sand;
	[SerializeField] private GameObject wood;
	[SerializeField] private GameObject superWood;
	[SerializeField] private GameObject seaWeed;
	[SerializeField] private Sprite oneTexture;
	[SerializeField] private Sprite twoTexture;
	[SerializeField] private Sprite threeTexture;
	[SerializeField] private Sprite fourTexture;
	[SerializeField] private Sprite fiveTexture;
	[SerializeField] private Sprite sixTexture;
	[SerializeField] private Sprite sevenTexture;
	[SerializeField] private Sprite heightTexture;
	[SerializeField] private Sprite nineTexture;
	[SerializeField] private Sprite zeroTexture;
	[SerializeField] int iconChildNumber;

	public static UIInventory Instance;

	private void Awake()
	{
		Instance = this;
		seaShell.SetActive(false);
		sand.SetActive(false);
		wood.SetActive(false);
		superWood.SetActive(false);
		seaWeed.SetActive(false);
	}

	public void Notify()
	{
		int numberOfSeaShell = 0;
		int numberOfSand = 0;
		int numberOfWood = 0;
		int numberOfSuperWood = 0;
		int numberOfSeaWeed = 0;

		foreach (var items in Inventory.Instance.Items)
		{
			switch (items.Type)
			{
				case ItemType.Wood:
					++numberOfWood;
					break;
				case ItemType.SeaWeed:
					++numberOfSeaWeed;
					break;
				case ItemType.Sand:
					++numberOfSand;
					break;
				case ItemType.SeaShell:
					++numberOfSeaShell;
					break;
				case ItemType.SuperWood:
					++numberOfSuperWood;
					break;
				default:
					break;
			}
		}
		seaShell.SetActive(numberOfSeaShell != 0);
		sand.SetActive(numberOfSand != 0);
		wood.SetActive(numberOfWood != 0);
		superWood.SetActive(numberOfSuperWood != 0);
		seaWeed.SetActive(numberOfSeaWeed != 0);
		GameObject[] gameObjects = new GameObject[] { seaShell, sand, wood, superWood, seaWeed };
		int[] numbers = new int[] { numberOfSeaShell, numberOfSand, numberOfWood, numberOfSuperWood, numberOfSeaWeed };

		for (int i = 0; i < gameObjects.Length; i++)
		{
			gameObjects[i].SetActive(numbers[i] != 0);
			Image icon = gameObjects[i].transform.GetChild(iconChildNumber).GetComponent<Image>();

			switch (numbers[i])
			{
				case 1:
					icon.sprite = oneTexture;
					break;
				case 2:
					icon.sprite = twoTexture;
					break;
				case 3:
					icon.sprite = threeTexture;
					break;
				case 4:
					icon.sprite = fourTexture;
					break;
				case 5:
					icon.sprite = fiveTexture;
					break;
				case 6:
					icon.sprite = sixTexture;
					break;
				case 7:
					icon.sprite = sevenTexture;
					break;
				case 8:
					icon.sprite = heightTexture;
					break;
				case 9:
					icon.sprite = nineTexture;
					break;
				case 0:
					icon.sprite = zeroTexture;
					break;
				default:
					break;
			}
		}
	}
}
