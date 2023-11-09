using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

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

        }
    }
}
