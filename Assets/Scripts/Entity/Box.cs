using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Box : MonoBehaviour, IInterect
{
    [SerializeField] private Sprite _openSprite;
    [SerializeField] private Sprite _closeSprite;
    [SerializeField] private bool _isOpen;
    [SerializeField] private Coin _loot;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;

    public void Interect()
    {
        if (!_isOpen)
        {
            float _offsetSpawnY = 1.2f;

            _isOpen = true;
            _spriteRenderer.sprite = _openSprite;
            _collider2D.isTrigger = true;
            if (_loot)
                Instantiate(_loot, new Vector2(transform.position.x, transform.position.y + _offsetSpawnY), _loot.transform.rotation);
            Destroy(this);
        }
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _collider2D = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _isOpen ? _openSprite : _closeSprite;
    }
}
