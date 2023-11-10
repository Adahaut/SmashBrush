using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private Transform _myTransform;
    private float _coolDown;

    private void Awake()
    {
        _myTransform = _playerMovement.GetComponentInParent<Transform>();
        _coolDown = 0f;

    }
    public void OnMoveCharacter(InputAction.CallbackContext context)
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

    public void OnJump(InputAction.CallbackContext context)
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

    public void OnFistPunch(InputAction.CallbackContext ctx)
    {
        if (ctx.started && Time.time > _coolDown)
        {
            FistPunch fistPunch = new(_myTransform.position, _playerMovement._isFacingLeft);
            fistPunch.Execute();
            _coolDown = Time.time + fistPunch.GetSpeed();
        }
    }

    public void OnFeetPunch(InputAction.CallbackContext ctx)
    {
        if (ctx.started && Time.time > _coolDown)
        {
            FeetPunch feetPunch = new(_myTransform.position, _playerMovement._isFacingLeft);
            feetPunch.Execute();
            _coolDown = Time.time + feetPunch.GetSpeed();
        }
    }
}
