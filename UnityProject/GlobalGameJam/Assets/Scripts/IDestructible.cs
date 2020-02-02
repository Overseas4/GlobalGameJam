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

public interface IDestructible
{
    DestructionState DestructionState { get; set; }
    float Health { get; set; }
    void TakeDamage(float damageTaken);
    void RepairDamage(float repairAmount);
    void BreakDownTo(DestructionState newState);
    void RepairBackTo(DestructionState newState);

}
