using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    [SerializeField] private List<Color> _colors = new List<Color>();
    [SerializeField] private List<float> _ratios = new List<float>();
    private Color RandomColor
    {
        get
        {
            Color color = Color.white;
            if (Colors.Count != 0)
            {
                if (_ratios.Count != Colors.Count)
                {
                    color = _colors[Random.Range(0, _colors.Count)];
                }
                else
                {
                    float random01 = Random.Range(0f, 1f);
                    float value = 0f;
                    for (int i = 0; i < _ratios.Count; i++)
                    {
                        value += _ratios[i];
                        if (random01 < value)
                        {
                            color = Colors[i];
                            break;
                        }
                    }
                }
            }
            return color;
        }
    }
    public List<Color> Colors { get => _colors; set => _colors = value; }
    public List<float> Ratios { get => _ratios; set => _ratios = value; }
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
