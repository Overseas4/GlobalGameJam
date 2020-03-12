using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollWaterTexture : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curveY = AnimationCurve.Linear(0, 0, 10, 10);
    [SerializeField] private float _wavesTextureSpeed = 0.05f;
    [SerializeField] private float _normalWaveScaling = 1f;
    [SerializeField] private float _bigWaveScaling = 3.5f;

    [SerializeField] private int _nbWavesBeforeBigWave = 5;
    [SerializeField] private Vector2 _minMaxDamage = new Vector2(10f, 15f);
    [SerializeField] private float _damageMultiplierPerBigWave = 1.5f;

    private float _scrollNormalY = 0.62f;
    private float _scrollNormalX = 0.35f;
    private float _scrollNormal2Y = 0.42f;
    private float _scrollNormal2X = 0.53f;
    private float _resetTextureTimer = 0f;
    private float _timer = 0f;
    private float _bigWavesTimer = 0f;

    private int _wavesCount = 0;

    private Renderer _renderer = null;
    private Renderer Renderer { get => _renderer != null ? _renderer : _renderer = GetComponent<Renderer>(); }
    private Vector2 _offset = new Vector2();
    private Vector2 _offset2 = new Vector2();
    private Vector3 _initialPosition;
    private bool _isBigWave = false;

    void Start()
    {
        _offset = Renderer.material.mainTextureOffset;
        _initialPosition = transform.position;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        _offset = new Vector2(_timer * _wavesTextureSpeed * _scrollNormalX, _timer * _wavesTextureSpeed * _scrollNormalY);
        _offset2 = new Vector2(_timer * _wavesTextureSpeed* _scrollNormal2X, _timer * _wavesTextureSpeed * _scrollNormal2Y);
        Renderer.material.mainTextureOffset = _offset;
        Renderer.material.SetTextureOffset("_DetailAlbedoMap", _offset2);
        float y = _curveY.Evaluate(Time.timeSinceLevelLoad) * _normalWaveScaling;
        transform.position = Vector3.up * y + _initialPosition;
        _isBigWave = false;
        if (_timer > _nbWavesBeforeBigWave * 10f)
        {
            if (!_isBigWave)
            {
                eventController.Instance.OBJ_bigWave.Post(gameObject);
            }
            _isBigWave = true;
            _bigWavesTimer += Time.deltaTime;
            y = _curveY.Evaluate(Time.timeSinceLevelLoad) * _bigWaveScaling;
            transform.position = Vector3.up * y + _initialPosition;
            if(_bigWavesTimer > 10f)
            {
                _minMaxDamage *= _damageMultiplierPerBigWave;
                _bigWavesTimer = 0f;
                _timer = 0f;
            }
        }
    }
}
