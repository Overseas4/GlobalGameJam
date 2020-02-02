using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IDestructible
{
    [SerializeField] private DestructionState _desctructionState = DestructionState.New;
    [SerializeField] private float _health = 100f;

    public DestructionState DestructionState { get => _desctructionState; set => _desctructionState = value; }
    public float Health { get => _health; set => _health = value; }

    public void TakeDamage(float damageTaken)
    {
        Health -= damageTaken;
        switch (DestructionState)
        {
            case DestructionState.New:
                if(Health <= 0f)
                {
                    BreakDownTo(DestructionState.Broken);
                }
                else if(Health <= 33f)
                {
                    BreakDownTo(DestructionState.VeryDamaged);
                }
                else if (Health <= 66f)
                {
                    BreakDownTo(DestructionState.Damaged);
                }
                else if(Health < 100f)
                {
                    BreakDownTo(DestructionState.Damaged);
                }
                break;
            case DestructionState.Damaged:
                if (Health <= 0f)
                {
                    BreakDownTo(DestructionState.Broken);
                }
                else if (Health <= 33f)
                {
                    BreakDownTo(DestructionState.VeryDamaged);
                }
                else if (Health <= 66f)
                {
                    BreakDownTo(DestructionState.Damaged);
                }
                break;
            case DestructionState.VeryDamaged:
                if (Health <= 0f)
                {
                    BreakDownTo(DestructionState.Broken);
                }
                else if (Health <= 33f)
                {
                    BreakDownTo(DestructionState.VeryDamaged);
                }
                break;
            case DestructionState.Broken:
                if (Health <= 0f)
                {
                    BreakDownTo(DestructionState.Broken);
                }
                break;
            default:
                break;
        }
    }

    public void BreakDownTo(DestructionState newState)
    {
    }

    public void RepairDamage(float repairAmount)
    {
        Health += repairAmount;
        switch (DestructionState)
        {
            case DestructionState.Damaged:
                if (Health == 100f)
                {
                    BreakDownTo(DestructionState.Broken);
                }
                else if (Health <= 33f)
                {
                    BreakDownTo(DestructionState.VeryDamaged);
                }
                else if (Health <= 66f)
                {
                    BreakDownTo(DestructionState.Damaged);
                }
                break;
            case DestructionState.VeryDamaged:
                if (Health <= 0f)
                {
                    BreakDownTo(DestructionState.Broken);
                }
                else if (Health <= 33f)
                {
                    BreakDownTo(DestructionState.VeryDamaged);
                }
                break;
            case DestructionState.Broken:
                if (Health <= 0f)
                {
                    BreakDownTo(DestructionState.Broken);
                }
                break;
            default:
                break;
        }
    }

    public void RepairBackTo(DestructionState newState)
    {
    }
}
