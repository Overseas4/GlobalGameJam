using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollWaterTexture : MonoBehaviour
{
    [SerializeField] private float _wavesSpeed = 0.05f;
    [SerializeField] private float _cycleDuration = 36f;
    [SerializeField] private float _waveHeight = 2f;

    [SerializeField] private float _bigWaveHeight = 3.5f;
    [SerializeField] private float _bigWavesInterval = 120f;

    private float _scrollNormalY = 0.62f;
    private float _scrollNormalX = 0.35f;
    private float _scrollNormal2Y = 0.42f;
    private float _scrollNormal2X = 0.53f;
    private float _resetTextureTimer = 0f;
    private float _timer = 0f;
    private const float _degreesOfCycle = 360f;
    private float _bigWavesTimer = 0f;

    private float _degreesPerSecond = 0f;
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
        _degreesPerSecond = _degreesOfCycle / _cycleDuration;
    }

    void Update()
    {
        _timer += Time.deltaTime * _wavesSpeed;
        _offset = new Vector2(_timer * _scrollNormalX, _timer * _scrollNormalY);
        _offset2 = new Vector2(_timer * _scrollNormal2X, _timer * _scrollNormal2Y);

        _rotationPoint.transform.RotateAround(_initialPosition, Vector3.left, _degreesPerSecond * Time.deltaTime);

        if (_bigWavesTimer < _bigWavesInterval)
        {
            Renderer.material.mainTextureOffset = _offset;
            Renderer.material.SetTextureOffset("_DetailAlbedoMap", _offset2);
            transform.position = _initialPosition + _rotationPoint.transform.forward * _waveHeight;
        }
        else
        {
            Renderer.material.color = Color.red;
            Renderer.material.mainTextureOffset = _offset * 1.5f ;
            Renderer.material.SetTextureOffset("_DetailAlbedoMap", _offset2 * 1.5f);
            transform.position = _initialPosition + _rotationPoint.transform.forward * _bigWaveHeight;
            
            if(_bigWavesTimer > _bigWavesInterval + _cycleDuration)
            {
                _bigWavesTimer -= _bigWavesInterval + _cycleDuration;
            }
        }
        _bigWavesTimer += Time.deltaTime;
    }
}
