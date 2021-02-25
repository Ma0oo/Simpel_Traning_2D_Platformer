using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverByRigibody : MonoBehaviour, IMover
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Clamper _clamperSpeedY;

    public Vector2 Velocity => _rigidbody.velocity;

    public GameObject GameObject => gameObject;

    private Rigidbody2D _rigidbody;
    private float _diractionX;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_clamperSpeedY.IsUse)
        {
            Vector2 tempVelocity = Velocity;
            tempVelocity.y = _clamperSpeedY.Clamp(tempVelocity.y);
            _rigidbody.velocity = tempVelocity;
        }
        if (_diractionX != 0)
            MoveByX();
    }

    private void MoveByX()
    {
        Vector2 temp = _rigidbody.velocity;
        temp.x = _diractionX * _speed;
        _rigidbody.velocity = temp;
    }

    public void SetDiractionMoveX(float x)
    {
        _diractionX = x;
    }

    public void SetVelocityAndDiractionX(Vector2 velocity)
    {
        _diractionX = velocity.x;
        _rigidbody.velocity = velocity;
    }

    public void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }
}
