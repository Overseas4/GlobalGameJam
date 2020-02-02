using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCondition : MonoBehaviour
{
	bool youDie = false;

	[SerializeField] List<Destructible> destructibles = new List<Destructible>();

	[SerializeField] GameObject loseimage;

	void Update()
	{
		youDie = true;
		foreach (var item in destructibles)
		{
			if(item != null)
			{
				youDie = false;
			}
		}
		if (youDie)
		{
			loseimage.SetActive(true);
		}
	}
}
