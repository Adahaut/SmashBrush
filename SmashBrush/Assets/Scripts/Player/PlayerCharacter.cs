using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    public int _percent;
    public int _lifes = 3;
    private int _playerID;
    private Transform _myTransform;
    [SerializeField] private TextMeshProUGUI _playerName;
    public GameObject _ejectVFX;

    public PlayerController _controller;

    public TextMeshProUGUI _UI;
    public GameObject _lifeUI;
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
        Camera.main.GetComponent<CameraMovement>()._players.Add(this);
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
        _lifeUI = Instantiate(_lifeUI, GameObject.Find("Canvas").transform);
        _lifeUI.transform.SetParent(_UI.transform);
        if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 0)
        {
            _actualSpawnPoint = _spawnPoint1.position;
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(-850, 495, 0);
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 1;
            _playerID = 1;

            SetNamePlate("Player 1", Color.blue);

        }
        else if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 1)
        {
            _actualSpawnPoint = _spawnPoint2.position;
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(850, 495, 0);
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 2;
            _playerID = 2;

            SetNamePlate("Player 2", Color.red);
        }
        else if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 2)
        {
            _actualSpawnPoint = _spawnPoint3.position;
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(-850, -400, 0);
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 3;
            _playerID = 3;

            SetNamePlate("Player 3", Color.yellow);
        }
        else if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 3)
        {
            _actualSpawnPoint = _spawnPoint4.position;
            _UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(850, -400, 0);
            Camera.main.GetComponent<CameraMovement>()._nbPlayer = 4;
            _playerID = 4;

            SetNamePlate("Player 4", Color.green);
        }
        _lifeUI.GetComponent<RectTransform>().anchoredPosition = Vector2.down * 80f;
        _UI.text = "Player " + _playerID + " " + _percent + " %";
    }

    private void SetNamePlate(string name, Color color)
    {
        _playerName.text = name;
        _playerName.color = color;
        _UI.color = color;

        foreach (Transform child in _lifeUI.transform)
        {
            child.gameObject.GetComponent<Image>().color = color;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "WorldEdges")
        {
            //Debug.Log(_myTransform.position);
            _lifes--;

            Camera.main.GetComponent<CameraMovement>().SetShake();

            _lifeUI.transform.GetChild(_lifes).gameObject.SetActive(false);

            GameObject a = Instantiate(_ejectVFX, _myTransform.position, Quaternion.identity);
            a.GetComponent<ParticleSystem>().Play();
            if (_lifes == 0)
            {
                Camera.main.GetComponent<CameraMovement>()._nbPlayer -= 1;
                Camera.main.GetComponent<CameraMovement>()._players.Remove(this);
            }
            if (Camera.main.GetComponent<CameraMovement>()._nbPlayer == 1)
            {
                PlayerPrefs.SetString("Player", Camera.main.GetComponent<CameraMovement>()._players[0]._playerID.ToString());
                SceneManager.LoadScene(_gameOverScene);
            }
            StartCoroutine(destroyA(a));
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

    private IEnumerator destroyA(GameObject a)
    {
            yield return new WaitForSeconds(0.5f);
            Destroy(a);
        if (_lifes != 0)
        {
            SpawnPlayer(_actualSpawnPoint);
        }
        else if(_lifes == 0)
        {
            Destroy(gameObject);
        }
    }

}
