using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverByRigibody : MonoBehaviour, IMover
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Clamper clamperSpeedY;

    public Vector2 Velocity => _rigidbody.velocity;

    private Rigidbody2D _rigidbody;
    private float _diractionX;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (clamperSpeedY.IsUse)
        {
            Vector2 tempVelocity = Velocity;
            tempVelocity.y = clamperSpeedY.Clamp(tempVelocity.y);
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

    public void SetVelocity(Vector2 velocity)
    {
        _diractionX = velocity.x;
        _rigidbody.velocity = velocity;
    }

    public void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }
}
