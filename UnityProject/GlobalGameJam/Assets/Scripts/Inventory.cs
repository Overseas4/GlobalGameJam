using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;
    public static Inventory Instance { get => _instance; set => _instance = value; }
    private List<InventoryStruct> _items;
    public List<InventoryStruct> Items { get => _items; set => _items = value; }
    public ControllerMark1 _player;
    public ControllerMark1 Player { get => _player != null ? _player :_player = GetComponentInParent<ControllerMark1>(); }
    public float TotalWeight = 0f;
    public float PlayerInitialMaxSpeed = 0f;

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
        _items = new List<InventoryStruct>();
        PlayerInitialMaxSpeed = Player.maxSpeed;
    }

    public void AddItem(InventoryStruct item)
    {
        Items.Add(item);
        TotalWeight += item.weight;
        const float percent = 0.01f;
        Player.maxSpeed = TotalWeight * percent * PlayerInitialMaxSpeed;
    }
}
