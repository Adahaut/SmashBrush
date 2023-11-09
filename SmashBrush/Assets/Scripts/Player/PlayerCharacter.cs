using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _coolDown)
        {
            FistPunch fistPunch = new(_myTransform.position);
            fistPunch.Execute();
            _coolDown = Time.time + fistPunch.GetSpeed();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > _coolDown)
        {
            FeetPunch feetPunch = new(_myTransform.position);
            feetPunch.Execute();
            _coolDown = Time.time + feetPunch.GetSpeed();
        }
    }
}
