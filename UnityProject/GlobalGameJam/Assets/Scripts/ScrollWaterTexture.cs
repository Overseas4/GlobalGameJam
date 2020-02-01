using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollWaterTexture : MonoBehaviour
{
    [SerializeField] private float _scrollNormalY = 0.62f;
    [SerializeField] private float _scrollNormalX = 0.35f;
    [SerializeField] private float _scrollNormal2Y = 0.42f;
    [SerializeField] private float _scrollNormal2X = 0.53f;
    [SerializeField] private float _resetTextureTimer = 0f;
    private float _timer = 0f;
    private float _distanceFromRotationPoint = 2f;

    [SerializeField] private float _cicleSpeed = 10f;
    [SerializeField] private float _wavesSpeed = 0.05f;
    private Renderer _renderer = null;
    private Renderer Renderer { get => _renderer != null ? _renderer : _renderer = GetComponent<Renderer>(); }
    private Vector2 _offset = new Vector2();
    private Vector2 _offset2 = new Vector2();
    private Vector3 _initialPosition;
    private GameObject _rotationPoint;
    void Start()
    {
        _offset = Renderer.material.mainTextureOffset;
        _initialPosition = transform.position;
        _rotationPoint = new GameObject();
        _rotationPoint.transform.SetParent(transform.parent);
        _rotationPoint.transform.position = transform.position;
    }
    void Update()
    {
        _timer += Time.deltaTime * _wavesSpeed;
        _offset = new Vector2(_timer * _scrollNormalX, _timer * _scrollNormalY);
        _offset2 = new Vector2(_timer * _scrollNormal2X, _timer * _scrollNormal2Y);
        Renderer.material.mainTextureOffset = _offset;
        Renderer.material.SetTextureOffset("_DetailAlbedoMap", _offset2);
        _rotationPoint.transform.RotateAround(_initialPosition, Vector3.left, _cicleSpeed * Time.deltaTime);
        transform.position = _initialPosition + _rotationPoint.transform.forward * _distanceFromRotationPoint;
    }
}
