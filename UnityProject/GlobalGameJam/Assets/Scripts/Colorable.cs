using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class SerializedColor
//{
//    public float[] colorArr;

//    public SerializedColor()
//    {
//        colorArr = new float[4];
//    }
//}

public class Colorable : MonoBehaviour
{
    [SerializeField] private List<Color> colors = new List<Color>();

    //private SerializedColor serializedColors;

    //Color32 color = new Color32();
    //float[] colorAsFloat = new float[4];

    //private Color32 DeserializedColor()
    //{
    //    Color32 color = new Color32;
    //    color.r = (byte)colorAsFloat[0];
    //    color.g = (byte)colorAsFloat[1];
    //    color.b = (byte)colorAsFloat[2];
    //    color.a = (byte)colorAsFloat[3];
    //    return color;
    //}

    //private float[] SerializeColor(Color32 color)
    //{
    //    float[] colorAsFloat = new float[4];
    //    colorAsFloat[0] = color.r;
    //    colorAsFloat[1] = color.g;
    //    colorAsFloat[2] = color.b;
    //    colorAsFloat[3] = color.a;
    //    return colorAsFloat;
    //}
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = colors[Random.Range(0,colors.Count)];
    }
}
