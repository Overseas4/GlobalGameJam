using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollWaterTexture : MonoBehaviour
{

    [SerializeField] private AnimationCurve _curveY = AnimationCurve.Linear(0, 0, 10, 10);
    [SerializeField] private float _wavesSpeed = 0.05f;
    [SerializeField] private float _cycleDuration = 36f;
    [SerializeField] private float _waveHeight = 2f;

    [SerializeField] private float _bigWaveHeight = 3.5f;

    private float _scrollNormalY = 0.62f;
    private float _scrollNormalX = 0.35f;
    private float _scrollNormal2Y = 0.42f;
    private float _scrollNormal2X = 0.53f;
    private float _resetTextureTimer = 0f;
    private float _timer = 0f;
    private float _bigWavesTimer = 0f;

    private int _wavesCount = 0;
    [SerializeField] private int _nbWavesBeforeBigWave = 5;

    private Renderer _renderer = null;
    private Renderer Renderer { get => _renderer != null ? _renderer : _renderer = GetComponent<Renderer>(); }
    private Vector2 _offset = new Vector2();
    private Vector2 _offset2 = new Vector2();
    private Vector3 _initialPosition;

    void Start()
    {
        _offset = Renderer.material.mainTextureOffset;
        _initialPosition = transform.position;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        _offset = new Vector2(_timer * _wavesSpeed * _scrollNormalX, _timer * _wavesSpeed * _scrollNormalY);
        _offset2 = new Vector2(_timer * _wavesSpeed* _scrollNormal2X, _timer * _wavesSpeed * _scrollNormal2Y);
        Renderer.material.mainTextureOffset = _offset;
        Renderer.material.SetTextureOffset("_DetailAlbedoMap", _offset2);
        float y = _curveY.Evaluate(Time.timeSinceLevelLoad) * 0.35f + _initialPosition.y;
        transform.position = Vector3.up * y + _initialPosition;
        if (_timer > 60f)
        {
            _bigWavesTimer += Time.deltaTime;
            y = _curveY.Evaluate(Time.timeSinceLevelLoad) * 0.85f + _initialPosition.y;
            transform.position = Vector3.up * y + _initialPosition;
            if(_bigWavesTimer > 10f)
            {
                _timer = 0f;
            }
        }
    }
}
