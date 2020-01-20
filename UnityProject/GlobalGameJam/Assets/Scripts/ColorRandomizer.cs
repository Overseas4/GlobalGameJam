using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    [SerializeField] private List<Color> colors = new List<Color>(3);
    private Color RandomColor { get => colors[Random.Range(0, colors.Count)]; }

    void Start()
    {
        SetRandomColor();
    }

    void Update()
    {
        ResetOnEscape();
    }

    void SetRandomColor()
    {
        GetComponent<MeshRenderer>().material.color = RandomColor;
    }

    void ResetOnEscape()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SetRandomColor();
        }
    }
}
