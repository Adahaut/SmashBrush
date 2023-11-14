using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerCharacter : MonoBehaviour
{
    public int _percent;
    public int _lifes = 3;
    private int _playerID;
    private Transform _myTransform;

    public PlayerController _controller;

    public TextMeshProUGUI _UI;
    public Transform _spawnPoint1;
    public Transform _spawnPoint2;
    public Transform _spawnPoint3;
    public Transform _spawnPoint4;
    private Vector3 _actualSpawnPoint;
    private void Awake()
    {
        _spawnPoint1 = GameObject.Find("SpawnPoint1").transform;
        _spawnPoint2 = GameObject.Find("SpawnPoint2").transform;
        _spawnPoint3 = GameObject.Find("SpawnPoint3").transform;
        _spawnPoint4 = GameObject.Find("SpawnPoint4").transform;
        _controller = GetComponent<PlayerController>();
        _myTransform = transform;
        CreateUI();
    }

    private void Start()
    {
        SpawnPlayer(_actualSpawnPoint);
    }

    private void Update()
    {
        _UI.text = "Player " + _playerID + " " + _percent + " %";
    }

    private void CreateUI()
    {
        _UI = Instantiate(_UI, GameObject.Find("Canvas").transform);
        if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 0)
        {
            _actualSpawnPoint = _spawnPoint1.position;
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(-850, 495, 0);
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 1;
            _playerID = 1;
            
        }
        else if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 1)
        {
            _actualSpawnPoint = _spawnPoint2.position;
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(890, 495, 0);
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 2;
            _playerID = 2;
        }
        else if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 2)
        {
            _actualSpawnPoint = _spawnPoint3.position;
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(-850, -495, 0);
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 3;
            _playerID = 3;
        }
        else if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 3)
        {
            _actualSpawnPoint = _spawnPoint4.position;
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(890, -495, 0);
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 4;
            _playerID = 4;
        }
        _UI.text = "Player " + _playerID + " " + _percent + " %";

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "WorldEdges")
        {
            _lifes--;
            SpawnPlayer(_actualSpawnPoint);
        }
    }

    private void SpawnPlayer(Vector3 pos)
    {
        _myTransform.position = pos;
        GetComponent<PlayerMovement>()._velocity = Vector2.zero;
    }

}
