using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totoro : MonoBehaviour
{
    [SerializeField]AK.Wwise.Event akEvent;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            akEvent.Post(gameObject);
        }
    }
}
