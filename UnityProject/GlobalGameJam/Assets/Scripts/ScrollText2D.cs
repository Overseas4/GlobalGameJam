using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText2D : MonoBehaviour
{
    [SerializeField] private float _scrollY = 0.5f;
    [SerializeField] private float _scrollX = 0f;
    [SerializeField] private float _resetTextureTimer = 0f;
    private float _timer = 0f;
    private Renderer _renderer = null;
    private Renderer Renderer { get => _renderer != null ? _renderer : _renderer = GetComponent<Renderer>(); }
    private Vector2 _offset = new Vector2();
    void Start()
    {
        _offset = Renderer.material.mainTextureOffset;
    }
    void Update()
    {
        _timer += Time.deltaTime;
        _timer = _timer < _resetTextureTimer && _resetTextureTimer != 0f ? _timer : 0f;
        _offset = new Vector2(_timer * _scrollX , _timer * _scrollY);
        Renderer.material.mainTextureOffset = _offset;
    }
} 
