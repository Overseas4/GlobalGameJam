using UnityEngine;

public class Destructible : MonoBehaviour, IDestructible
{

    [SerializeField] public DestructionState _destructionState = DestructionState.New;
    [SerializeField] public DestructibleObjects _desctructionObject = DestructibleObjects.CastleWall;
    [SerializeField] public float _health = 100f;
    [SerializeField] public GameObject _shapeNew = null;
    [SerializeField] public GameObject _shapeDamaged = null;
    [SerializeField] public GameObject _shapeVeryDamaged = null;
    private bool _destroyed = false;
    private int waterLayerMask = 4;
    public DestructionState DestructionState { get => _destructionState; set => _destructionState = value; }
    public float Health { get => _health; set => _health = value; }
    public bool IsInWater { get; private set; }

    public GameObject ShapeNew { get => _shapeNew; set => _shapeNew = value; }
    public GameObject ShapeDamaged { get => _shapeDamaged; set => _shapeDamaged = value; }
    public GameObject ShapeVeryDamaged { get => _shapeVeryDamaged; set => _shapeVeryDamaged = value; }

    public bool Destroyed { get => _destroyed; set => _destroyed = value; }

    public void TakeDamage(float damageTaken)
    {
        Health -= damageTaken;
        Health = Health > 0f ? Health : 0f;

        switch (DestructionState)
        {
            case DestructionState.New:
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
                else if (Health < 100f)
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
        Health = Health < 100f ? Health : 100f;
        switch (DestructionState)
        {
            case DestructionState.Broken:
                if (Health >= 100f)
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
        _shapeNew.SetActive(false);
        _shapeDamaged.SetActive(false);
        _shapeVeryDamaged.SetActive(false);

        _destructionState = newState;
        switch (newState)
        {
            case DestructionState.Damaged:
                _shapeDamaged.SetActive(true);
                break;
            case DestructionState.VeryDamaged:
                _shapeVeryDamaged.SetActive(true);
                break;
            case DestructionState.Broken:
                Destroy(this);
                break;
        }
    }

    public void RepairBackTo(DestructionState newState)
    {
        _shapeNew.SetActive(false);
        _shapeDamaged.SetActive(false);
        _shapeVeryDamaged.SetActive(false);
        switch (newState)
        {
            case DestructionState.New:
                _shapeNew.SetActive(true);
                break;
            case DestructionState.Damaged:
                _shapeDamaged.SetActive(true);
                break;
            case DestructionState.VeryDamaged:
                _shapeVeryDamaged.SetActive(true);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == waterLayerMask)
        {
            IsInWater = true;
            ScrollWaterTexture water = other.GetComponent<ScrollWaterTexture>();
            TakeDamage(Random.Range(water._minMaxDamage.x, water._minMaxDamage.y));
        }
    }

    public void ChangeToThisPrefab(GameObject prefab)
    {

        GameObject newPrefab = Instantiate(prefab, transform);
    }

    void Start()
    {
        _shapeNew.SetActive(true);
        _shapeDamaged.SetActive(false);
        _shapeVeryDamaged.SetActive(false);
    }
}
