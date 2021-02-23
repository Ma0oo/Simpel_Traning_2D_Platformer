using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveDiraction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(_moveDiraction!=Vector2.zero)
            _rigidbody.AddForce(_moveDiraction * _speed);
    }
    public void SetDiractionMove(Vector2 newDirection)
    {
        _moveDiraction = newDirection.normalized;
    }
    public void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }
}
