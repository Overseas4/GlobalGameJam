using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DestructionState
{
    New,
    Damaged,
    VeryDamaged,
    Broken,
}

public enum DestructibleObjects
{
    Door,
    CastleWall,
    WoodWall,
}

public interface IDestructible
{
    DestructionState DestructionState { get; set; }
    float Health { get; set; }
    void TakeDamage(float damageTaken);
    void RepairDamage(float repairAmount);
    void BreakDownTo(DestructionState newState);
    void RepairBackTo(DestructionState newState);
    GameObject ShapeNew { get; set; }
    GameObject ShapeDamaged { get; set; }
    GameObject ShapeVeryDamaged { get; set; }
    bool Destroyed { get; set; }
}
