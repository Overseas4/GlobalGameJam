using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;
    public static Inventory Instance { get => _instance; set => _instance = value; }
    private List<IIventoryItem> _items;
    public List<IIventoryItem> Items { get => _items; set => _items = value; }

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (_instance != null)
        {
            Destroy(_instance);
        }
        _instance = this;
        _items = new List<IIventoryItem>();
    }
}
