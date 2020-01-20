using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPrefab = null;
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private int _ammountPerInterval = 1;
    private List<GameObject> _spawns = new List<GameObject>();
    private List<Transform> _spawnPointsList = new List<Transform>();
    private Transform[] _spawnPoints = null;
    public GameObject SpawnObject { get => Instantiate(_spawnPrefab); }
    private static float timer = 0f;
    Transform GetSpawnPoint
    {
        get
        {
            int random = Random.Range(0, _spawnPointsList.Count);
            Transform spawnpoint = _spawnPointsList[random];
            _spawnPointsList.RemoveAt(random);
            if (_spawnPointsList.Count == 0)
            {
                for (int i = 0; i < _spawnPoints.Length; i++)
                {
                    _spawnPointsList.Add(_spawnPoints[i]);
                }
            }
            return spawnpoint;
        }
    }

    void Awake()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPointsList.Add(_spawnPoints[i]);
        }
    }

    void Update()
    {
        ResetOnEscape();
        timer += Time.deltaTime;
        if(timer > _spawnInterval)
        {
            timer -= _spawnInterval;
            for(int i = 0; i < _ammountPerInterval; i++)
            {
                GameObject spawn = SpawnObject;
                SetRandomPosition(ref spawn);
                _spawns.Add(spawn);
            }
        }
    }

    void SetRandomPosition(ref GameObject obj)
    {
        Transform randomSpawnPoint = GetSpawnPoint;
        obj.transform.position = randomSpawnPoint.position;
        obj.transform.rotation = randomSpawnPoint.rotation;
    }

    void ResetSpawnsList()
    {
        for (int i = 0; i < _spawns.Count; i++)
        {
            Destroy(_spawns[i]);
            _spawns.RemoveAt(i);
        }
        _spawns = new List<GameObject>();
    }
    void ResetSpawnPositionsList()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPointsList.Add(_spawnPoints[i]);
        }
    }
    void ResetOnEscape()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ResetSpawnsList();
            ResetSpawnPositionsList();
            timer = 0f;
        }
    }
}
