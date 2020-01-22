using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    [System.Serializable]
    public struct ColorWithRatio
    {
        public Color color;
        public float ratio;
    }

    [SerializeField] private List<ColorWithRatio> _colorsWithRatio = new List<ColorWithRatio>();
    public List<ColorWithRatio> ColorsWithRatio { get => _colorsWithRatio; set => _colorsWithRatio = value; }

    private Color RandomColor
    {
        get
        {
            Color color = Color.white;
            if (ColorsWithRatio.Count != 0)
            {
                float sumOfRatios = 0f;
                for (int i = 0; i < ColorsWithRatio.Count; i++)
                {
                    sumOfRatios += ColorsWithRatio[i].ratio;
                }

                if (sumOfRatios == 0f)
                {
                    int randomIndex = Random.Range(0, ColorsWithRatio.Count);
                    color = ColorsWithRatio[randomIndex].color;
                }
                else
                {
                    float random = Random.Range(0f, sumOfRatios);
                    float value = 0f;
                    for (int i = 0; i < ColorsWithRatio.Count; i++)
                    {
                        value += ColorsWithRatio[i].ratio;
                        if (random < value)
                        {
                            color = ColorsWithRatio[i].color;
                            break;
                        }
                    }
                }

            }
            return color;
        }
    }

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
