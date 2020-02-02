using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkEventOnClick : MonoBehaviour
{
	[SerializeField] AK.Wwise.Event eventToPlay;
	public void Click()
	{
		eventToPlay.Post(gameObject);
	}
}
