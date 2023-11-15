using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    public int _percent;
    public int _lifes = 3;
    private int _playerID;
    private Transform _myTransform;
    [SerializeField] private TextMeshProUGUI _playerName;

    public PlayerController _controller;

    public TextMeshProUGUI _UI;
    public Transform _spawnPoint1;
    public Transform _spawnPoint2;
    public Transform _spawnPoint3;
    public Transform _spawnPoint4;
    private Vector3 _actualSpawnPoint;
    public int _gameOverScene;
    private void Awake()
    {
        _spawnPoint1 = GameObject.Find("SpawnPoint1").transform;
        _spawnPoint2 = GameObject.Find("SpawnPoint2").transform;
        _spawnPoint3 = GameObject.Find("SpawnPoint3").transform;
        _spawnPoint4 = GameObject.Find("SpawnPoint4").transform;
        _controller = GetComponent<PlayerController>();
        _myTransform = transform;
        CreateUI();
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
            _UI.color = Color.blue;
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 1;
            _playerID = 1;

            SetNamePlate("Player 1", Color.blue);

        }
        else if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 1)
        {
            _actualSpawnPoint = _spawnPoint2.position;
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(890, 495, 0);
            _UI.color = Color.red;
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 2;
            _playerID = 2;

            SetNamePlate("Player 2", Color.red);
        }
        else if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 2)
        {
            _actualSpawnPoint = _spawnPoint3.position;
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(-850, -470, 0);
            _UI.color = Color.yellow;
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 3;
            _playerID = 3;

            SetNamePlate("Player 3", Color.yellow);
        }
        else if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 3)
        {
            _actualSpawnPoint = _spawnPoint4.position;
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(890, -470, 0);
            _UI.color = Color.green;
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 4;
            _playerID = 4;

            SetNamePlate("Player 4", Color.green);
        }
        _UI.text = "Player " + _playerID + " " + _percent + " %";
    }

    private void SetNamePlate(string name, Color color)
    {
        _playerName.text = name;
        _playerName.color = color;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "WorldEdges")
        {
            Debug.Log(_myTransform.position);
            _lifes--;
            if (_lifes == 0)
            {
                Camera.main.GetComponent<CameraMovement>()._nbPlayer -= 1;
                Destroy(gameObject);
            }
            if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 1)
            {
                PlayerPrefs.SetString("Player", _playerID.ToString());
                SceneManager.LoadScene(_gameOverScene);
            }
            SpawnPlayer(_actualSpawnPoint);
        }
        else
        {
            return;
        }
    }

    private void SpawnPlayer(Vector3 pos)
    {
        _myTransform.position = pos;
        GetComponent<PlayerMovement>()._velocity = Vector2.zero;
        _percent = 0;
    }

}
