using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _bodyPart;

    private PlayerMovement _playerMovement;

    private Transform _myTransform;
    private float _coolDown;
    public float _stunTime;
    public bool _isStun;
    public GameObject _panel;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _myTransform = _playerMovement.GetComponentInParent<Transform>();
        _coolDown = 0f;
        _isStun = false;

        _panel = Camera.main.GetComponent<CameraMovement>()._panel;
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
                GetComponent<AudioSource>().Play();
                FistPunch fistPunch = new(_myTransform.position, _playerMovement._isFacingLeft);
                _playerMovement._velocity.x = _playerMovement._isFacingLeft ? -fistPunch.GetForward() : fistPunch.GetForward();
                fistPunch.Execute();
                _coolDown = Time.time + fistPunch.GetSpeed();

                StartCoroutine(_bodyPart.GetComponent<PlayerAnimation>().FistAnimation(_playerMovement._isFacingLeft, _myTransform.position, fistPunch.GetRange()));
            }
        }

    }

    public void OnFeetPunch(InputAction.CallbackContext ctx)
    {
        if (!_isStun)
        {
            if (ctx.started && Time.time > _coolDown)
            {
                GetComponent<AudioSource>().Play();
                FeetPunch feetPunch = new(_myTransform.position, _playerMovement._isFacingLeft);
                _playerMovement._velocity.x = _playerMovement._isFacingLeft ? -feetPunch.GetForward() : feetPunch.GetForward();
                feetPunch.Execute();
                _coolDown = Time.time + feetPunch.GetSpeed();

                StartCoroutine(_bodyPart.GetComponent<PlayerAnimation>().FootAnimation(_playerMovement._isFacingLeft, _myTransform.position, feetPunch.GetRange()));
            }
        }

    }

    public void OnMenuPauseOpen(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                _panel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                _panel.SetActive(false);
            }
        }
    }
}
