using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private Transform[] _spawnPoints = null;
    private GameObject _player = null;
    private Transform RandomSpawnPoint { get =>_spawnPoints.Length > 1 ? _spawnPoints[Random.Range(1, _spawnPoints.Length)] : _spawnPoints[0]; }
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
            Rigidbody rb = Player.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
