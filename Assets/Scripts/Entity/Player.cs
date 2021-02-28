using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        _spriteRenderer.color = Color.red;
        _animator.enabled = false;
        if (TryGetComponent(out IMover mover))
            mover.MonoComponentMover.enabled = false;
        RaycastHit2D[] result = new RaycastHit2D[10];
        int invertDirection = -1;
        GetComponent<Collider2D>().Cast(Vector2.up * invertDirection, result, 10);
        foreach (var item in result)
        {
            if (!item)
                continue;
            if (item.collider.TryGetComponent(out Tilemap tilemap))
                transform.position = item.centroid;
        }
    }
}
