using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public List<Transform> _targets = new List<Transform>();
    [SerializeField] private Vector3 _offSet;
    public GameObject _panel;

    public List<PlayerCharacter> _players;
    public int _nbPlayer = 0;

    private Vector3 _velocity;
    private float _smoothTime = 0.9f;

    private float _minZoom = 40f;

    private float _maxZoom = 25f;

    private float _zoomLimit = 50f;

    private float _distance = 0;
    private Transform _myTransform;

    private Camera _cam;

    private float _shakeDuration = 0.5f;
    private float _shakeTimer = 0;
    private bool _isShaking;

    private void Awake()
    {
        _players = new List<PlayerCharacter>();
        Time.timeScale = 1;
        _targets.Clear();
        _myTransform = transform;
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (_targets.Count > 0)
        {
            if (GetFurthestPlayer() <= 15f)
            {
                Vector3 centerPoint = GetCenterPoint();
                Vector3 newPosition = centerPoint + _offSet;

                if (!_isShaking)
                {
                    _myTransform.position = Vector3.SmoothDamp(_myTransform.position, newPosition, ref _velocity, _smoothTime);
                }
                else
                {
                    _shakeTimer += Time.deltaTime;
                    _myTransform.position = new Vector3(newPosition.x + Mathf.PerlinNoise(30 * Time.time, 0), newPosition.y + Mathf.PerlinNoise(30 * Time.time, 0), _myTransform.position.z);

                    if (_shakeTimer >= _shakeDuration)
                    {
                        _shakeTimer = 0f;
                        _isShaking = false;
                    }
                }
            }

            float newZoom = Mathf.Lerp(_maxZoom, _minZoom, GetGreatestDistance() / _zoomLimit);
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, newZoom, Time.deltaTime);
        }
    }

    private Vector3 GetCenterPoint()
    {
        if (_targets.Count == 1)
        {
            return _targets[0].position;
        }

        var bounds = new Bounds(_targets[0].position, Vector3.zero);

        for(int i = 0; i < _targets.Count; i++)
        {
            bounds.Encapsulate(_targets[i].position);
        }

        return bounds.center;
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(_targets[0].position, Vector3.zero);

        for (int i =  0; i < _targets.Count; ++i)
        {
            bounds.Encapsulate(_targets[i].position);
        }

        return bounds.size.x;
    }

    private float GetFurthestPlayer()
    {
        Transform previousTarget = _targets[0];
        if ( _targets.Count > 1)
        {
            _distance = Mathf.Abs(_targets[1].position.y - previousTarget.position.y);
        }
        else if (_targets.Count > 2)
        {
            for (int i = 2; i < _targets.Count; ++i)
            {
                if (_distance < Mathf.Abs(_targets[i].position.y - previousTarget.position.y))
                {
                    _distance = Mathf.Abs(_targets[i].position.y - previousTarget.position.y);
                }
                previousTarget = _targets[i];
            }
        }

        return _distance;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("player joined");
        _targets.Add(playerInput.transform);
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        _targets.Remove(playerInput.transform);
    }

    public void SetShake()
    {
        _isShaking = true;
    }
}
