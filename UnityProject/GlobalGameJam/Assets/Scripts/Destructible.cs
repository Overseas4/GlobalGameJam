using UnityEngine;

public class Destructible : MonoBehaviour, IDestructible
{

    [SerializeField] private DestructionState _desctructionState = DestructionState.New;
    [SerializeField] private DestructibleObjects _desctructionObject = DestructibleObjects.CastleWall;
    [SerializeField] private float _health = 100f;
    [SerializeField] private GameObject _shapeNew = null;
    [SerializeField] private GameObject _shapeDamaged = null;
    [SerializeField] private GameObject _shapeVeryDamaged = null;
    private bool _destroyed = false;
    public DestructionState DestructionState { get => _desctructionState; set => _desctructionState = value; }
    public float Health { get => _health; set => _health = value; }

    public GameObject ShapeNew { get => _shapeNew; set => _shapeNew = value; }
    public GameObject ShapeDamaged { get => _shapeDamaged; set => _shapeDamaged = value; }
    public GameObject ShapeVeryDamaged { get => _shapeVeryDamaged; set => _shapeVeryDamaged = value; }

    public bool Destroyed { get => _destroyed; set => _destroyed = value; }

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

    public void RepairDamage(float repairAmount)
    {
        Health += repairAmount;
        switch (DestructionState)
        {
            case DestructionState.Broken:
                if (Health == 100f)
                {
                    BreakDownTo(DestructionState.New);
                }
                else if (Health <= 66f)
                {
                    BreakDownTo(DestructionState.Damaged);
                }
                else if (Health <= 33f)
                {
                    BreakDownTo(DestructionState.VeryDamaged);
                }
                break;

            case DestructionState.VeryDamaged:
                if (Health == 100f)
                {
                    BreakDownTo(DestructionState.New);
                }
                else if (Health <= 66f)
                {
                    BreakDownTo(DestructionState.Damaged);
                }
                break;

            case DestructionState.Damaged:
                if (Health == 100f)
                {
                    BreakDownTo(DestructionState.New);
                }
                break;
            default:
                break;
        }
    }

    public void BreakDownTo(DestructionState newState)
    {
        switch (newState)
        {
            case DestructionState.New:
                break;
            case DestructionState.Damaged:
                break;
            case DestructionState.VeryDamaged:
                break;
            case DestructionState.Broken:
                break;
            default:
                break;
        }
    }

    public void RepairBackTo(DestructionState newState)
    {
        switch (newState)
        {
            case DestructionState.New:
                break;
            case DestructionState.Damaged:
                break;
            case DestructionState.VeryDamaged:
                break;
            case DestructionState.Broken:
                break;
            default:
                break;
        }
    }
}
