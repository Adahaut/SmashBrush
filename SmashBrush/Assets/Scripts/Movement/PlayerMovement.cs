using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;

    [Header("Movement attributs")]
    public float _acceleration = 100f;
    public float _maxSpeed = 3f;
    public float _dampFactor = 0.9f;

    private float _gravityAcceleration = 40f;
    private float _jumpHeight = 30f;
    private float _jumpDamping = 0.5f;

    public bool _isFacingLeft;
    private byte _jumpCount = 0;

    public Vector2 _velocity = Vector2.zero;
    private Vector2 _direction = Vector2.zero;

    private Rigidbody _rb;
    private Transform _myTransform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _myTransform = transform;
    }

    private void Update()
    {
        Move();
        if (!IsGrounded())
        {
            _velocity.y -= _gravityAcceleration * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        //Physics.IgnoreLayerCollision(7, 6, _velocity.y > 0 || !IsGrounded() && _velocity.y < 0);

        if (Physics.Raycast(_myTransform.position + (_myTransform.localScale / 2), Vector3.up, out RaycastHit hitInfo, 0.5f))
        {
            Physics.IgnoreCollision(_myTransform.GetComponent<Collider>(), hitInfo.collider, hitInfo.collider.tag == "Platform");
        }

        _rb.velocity = _velocity;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void Move()
    {
        if (_direction != Vector2.zero)
        {
            _velocity += _direction * _acceleration * Time.deltaTime;

            if (_velocity.x >= _maxSpeed)
            {
                _velocity.x = _maxSpeed;
            }
            else if (_velocity.x <= -_maxSpeed)
            {
                _velocity.x = -_maxSpeed;
            }
        }
        else
        {
            _velocity.x *= _dampFactor * Time.deltaTime;
        }
    }

    public void Jump()
    {
        if (_jumpCount < 2) 
        {
            _jumpCount++;

            _velocity.y = _jumpHeight;
        }
    }

    public void CancelJump()
    {
        if (_velocity.y > 0)
        {
            _velocity.y *= _jumpDamping;
        }
    }

    public bool IsGrounded()
    {
        if(Physics.BoxCast(_myTransform.position, _myTransform.localScale / 2, Vector3.down, Quaternion.identity, _myTransform.localScale.y / 2 + 0.1f, _groundMask))
        {
            if (_velocity.y < 0)
            {
                _velocity.y = 0;
                _jumpCount = 0;
            }

            return true;
        }
           
        return false;
    }
}
