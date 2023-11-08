using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _acceleration = 0.5f;

    private Vector2 _velocity = Vector2.zero;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

    }

    public void Move(Vector2 direction)
    {
        _velocity = direction * _acceleration;
    }
}
