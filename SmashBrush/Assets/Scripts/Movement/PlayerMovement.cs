using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;

    [Header("Movement attributs")]
    public float _acceleration = 0.2f;
    public float _maxSpeed = 3f;
    public float _dampFactor = 0.1f;

    private float _gravityAcceleration = 0.1f;
    private float _gravity = 20f;

    public bool _isFacingLeft;
    private bool _isJumping;
    private bool _jumpCanceled;

    private Vector2 _velocity = Vector2.zero;
    private Vector2 _direction = Vector2.zero;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _isJumping = false;
    }

    private void Update()
    {
        Move();
        if (!IsGrounded())
        {
            _velocity.y -= _gravityAcceleration;
        }
        else if (!IsGrounded() && _jumpCanceled)
        {
            _velocity.y -= _gravityAcceleration * 10;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(_velocity.x, _velocity.y, 0);
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void Move()
    {
        if (_direction != Vector2.zero)
        {
            _velocity += _direction * _acceleration;

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
            _velocity.x *= 1 - _dampFactor;
        }
    }

    public void Jump()
    {
        if (IsGrounded() && !_isJumping) 
        {
            _isJumping = true;
            _velocity.y = _gravity; 
        }
    }

    public void CancelJump()
    {
        if (_isJumping)
        {
            _jumpCanceled = true;
        }
    }

    public bool IsGrounded()
    {
        if(Physics.Raycast(transform.position, Vector3.down, transform.localScale.y / 2 + 0.5f, _groundMask))
        {
            if (_velocity.y < 0)
            {
                _velocity.y = 0;
                _isJumping = false;
                _jumpCanceled = false;
            }

            return true;
        }
           
        return false;
    }
}
