using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerCharacter : MonoBehaviour
{
    public int _percent;
    public int _lifes = 3;
    private Transform _myTransform;

    public PlayerController _controller;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _myTransform = transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "WorldEdges")
        {
            _lifes--;
            SpawnPlayer(new Vector3(15, 5, 19));
        }
    }

    private void SpawnPlayer(Vector3 pos)
    {
        _myTransform.position = pos;
        GetComponent<PlayerMovement>()._velocity = Vector2.zero;
    }

}
