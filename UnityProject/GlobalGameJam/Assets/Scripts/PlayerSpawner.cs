using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private Transform[] _spawnPoints = null;
    private GameObject _player = null;
    private Transform RandomSpawnPoint { get => _spawnPoints[Random.Range(0, _spawnPoints.Length)]; }
    public GameObject Player { get => _player != null ? _player : _player = Instantiate(Resources.Load("Prefabs/Player") as GameObject); }

    void Awake()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();
    }

    void Start()
    {
        SetRandomPosition();
    }

    void Update()
    {
        ResetOnEscape();
    }

    void SetRandomPosition()
    {
        Transform randomSpawnPoint = RandomSpawnPoint;
        Player.transform.position = randomSpawnPoint.position;
        Player.transform.rotation = randomSpawnPoint.rotation;
    }

    void ResetOnEscape()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SetRandomPosition();
        }
    }
}
