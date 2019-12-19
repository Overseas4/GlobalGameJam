using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totoro : MonoBehaviour
{
    [SerializeField]AK.Wwise.Event totoroClickEvent;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            totoroClickEvent.Post(gameObject);
        }
    }
}
