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
    private Renderer _renderer = null;
    private Renderer Renderer { get => _renderer != null ? _renderer : _renderer = GetComponent<Renderer>(); }
    private Vector2 _offset = new Vector2();
    private Vector2 _offset2 = new Vector2();
    void Start()
    {
        _offset = Renderer.material.mainTextureOffset;
    }
    void Update()
    {
        _timer += Time.deltaTime * 0.1f;
        //_timer = _timer < _resetTextureTimer && _resetTextureTimer != 0f ? _timer : 0f;
        _offset = new Vector2(_timer * _scrollNormalX, _timer * _scrollNormalY);
        _offset2 = new Vector2(_timer * _scrollNormal2X, _timer * _scrollNormal2Y);
        Renderer.material.mainTextureOffset = _offset;
        Renderer.material.SetTextureOffset("_DetailAlbedoMap", _offset2);
    }
}
