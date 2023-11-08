using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    public void OnMoveCharacter(InputAction.CallbackContext context)
    {
        Debug.Log("move");
        if (context.performed)
        {
            Debug.Log(context.ReadValue<Vector2>());
            _playerMovement.Move(context.ReadValue<Vector2>());
        }
        else
        {
            _playerMovement.Move(Vector2.zero);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("jump");
    }
}
