using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(Collider2D))]
public class Player : MonoBehaviour, IDamagabel
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInterect interect))
            interect.Interect();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out IInterect interect))
            interect.Interect();
    }

    public void ApplayDamage()
    {
        int deadLayer = 9;
        _spriteRenderer.color = Color.red;
        _animator.enabled = false;
        gameObject.layer = deadLayer;
        foreach (var item in GetComponents<MonoBehaviour>())
            item.enabled = false;
    }
}
