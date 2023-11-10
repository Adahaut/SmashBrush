using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private Transform _myTransform;
    private float _coolDown;
    public float _stunTime;
    public bool _isStun;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _myTransform = _playerMovement.GetComponentInParent<Transform>();
        _coolDown = 0f;
        _isStun = false;
    }

    private void Update()
    {
        if (Time.time > _stunTime)
        {
            _isStun = false;
        }
    }
    public void OnMoveCharacter(InputAction.CallbackContext context)
    {
        if (!_isStun)
        {
            if (context.performed)
            {
                _playerMovement.SetDirection(context.ReadValue<Vector2>());
                _playerMovement._isFacingLeft = context.ReadValue<Vector2>().x < 0 ? true : false;
            }
            else
            {
                _playerMovement.SetDirection(Vector2.zero);
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!_isStun)
        {
            if (context.started)
            {
                _playerMovement.Jump();
            }
            else
            {
                _playerMovement.CancelJump();
            }
        }
    }

    public void OnFistPunch(InputAction.CallbackContext ctx)
    {
        if (!_isStun)
        {
            if (ctx.started && Time.time > _coolDown)
            {
                FistPunch fistPunch = new(_myTransform.position, _playerMovement._isFacingLeft);
                _playerMovement._velocity.x = _playerMovement._isFacingLeft ? -fistPunch.GetForward() : fistPunch.GetForward();
                fistPunch.Execute();
                _coolDown = Time.time + fistPunch.GetSpeed();
            }
        }

    }

    public void OnFeetPunch(InputAction.CallbackContext ctx)
    {
        if (!_isStun)
        {
            if (ctx.started && Time.time > _coolDown)
            {
                FeetPunch feetPunch = new(_myTransform.position, _playerMovement._isFacingLeft);
                _playerMovement._velocity.x = _playerMovement._isFacingLeft ? -feetPunch.GetForward() : feetPunch.GetForward();
                feetPunch.Execute();
                _coolDown = Time.time + feetPunch.GetSpeed();
            }
        }

    }
}
