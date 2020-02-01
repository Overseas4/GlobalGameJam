using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : Interactible, IIventoryItem
{
    [SerializeField] private float _weight = 0f;
    [SerializeField] private float _durability = 0f;
    [SerializeField] private float _repairValue = 0f;
    [SerializeField] private float _damage = 0f;

    public float Durability { get => _durability; set => _durability = value; }
    public float RepairValue { get => _repairValue; set => _repairValue = value; }
    public float Damage { get => _damage; set => _damage = value; }
    public float Weight { get => _weight; set => _weight = value; }

    public override void Interact()
    {
        Inventory.Instance.Items.Add(this);
    }
}
