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
    private Transform _myTransform;

    public PlayerController _controller;

    public GameObject _UI;
    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _myTransform = transform;
        CreateUI();
    }

    private void CreateUI()
    {
        _UI = Instantiate(_UI, GameObject.Find("Canvas").transform);
        if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 0)
        {
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(-835, 495, 0);
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 1;
            return;
        }
        else if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 1)
        {
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(890, 495, 0);
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 2;
            return;
        }
        else if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 2)
        {
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(-835, -495, 0);
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 3;
            return;
        }
        else if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 3)
        {
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(890, -495, 0);
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 4;
            return;
        }
        _UI.GetComponent<TextMeshProUGUI>().text = "Player " + Camera.main.GetComponent<CameraMovement>()._nbPlayer;

    }

    private void Update()
    {
        _UI.GetComponent<TextMeshProUGUI>().text = "Player " + Camera.main.GetComponent<CameraMovement>()._nbPlayer;
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
