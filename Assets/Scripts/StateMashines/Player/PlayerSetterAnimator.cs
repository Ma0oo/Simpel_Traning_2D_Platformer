using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerSetterAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _playerInput.KeyRightPressed += OnKeyRightPressed;
        _playerInput.KeyLeftPressed += OnKeyLeftPressed;
    }

    private void OnDisable()
    {
        _playerInput.KeyRightPressed -= OnKeyRightPressed;
        _playerInput.KeyLeftPressed -= OnKeyLeftPressed;
    }

    private void Update()
    {
        _animator.SetFloat("SpeedX", _rigidbody.velocity.x);
        _animator.SetFloat("SpeedY", _rigidbody.velocity.y);
    }

    private void OnKeyRightPressed()
    {
        _animator.SetBool("LookToLeft", false);
    }

    private void OnKeyLeftPressed()
    {
        _animator.SetBool("LookToLeft", true);
    }
}
