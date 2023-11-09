using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
    public int _percent;
    public int _lifes;
    private Transform _myTransform;
    private float _coolDown;

    private void Awake()
    {
        _myTransform = transform;
        _coolDown = 0f;
    }

    public void OnAttackFistPunch(InputAction.CallbackContext ctx)
    {
        if (Time.time > _coolDown && ctx.started)
        {
            FistPunch fistPunch = new(_myTransform.position);
            fistPunch.Execute();
            _coolDown = Time.time + fistPunch.GetSpeed();
        }
    }

    public void OnAttackFeetPunch(InputAction.CallbackContext ctx)
    {
        if (Time.time > _coolDown && ctx.started)
        {
            FeetPunch feetPunch = new(_myTransform.position);
            feetPunch.Execute();
            _coolDown = Time.time + feetPunch.GetSpeed();
        }
    }
}
