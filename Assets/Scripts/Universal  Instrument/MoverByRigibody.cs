using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverByRigibody : MonoBehaviour, IMover
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Clamper _clamperSpeedY;

    public Vector2 Velocity => _rigidbody.velocity;

    public MonoBehaviour MonoComponentMover => this;

    private Rigidbody2D _rigidbody;
    private float _diractionX;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rigidbody.isKinematic = false;
    }

    private void OnDisable()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        Vector2 tempVelocity = Velocity;
        if (_clamperSpeedY.TryClamp(ref tempVelocity.y, Velocity.y))
            _rigidbody.velocity = tempVelocity;

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
