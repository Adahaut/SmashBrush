using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public List<Transform> _targets = new List<Transform>();
    [SerializeField] private Vector3 _offSet;

    public int _nbPlayer = 0;

    private Vector3 _velocity;
    private float _smoothTime = 0.5f;

    private float _minZoom = 40f;
    private float _maxZoom = 10f;
    private float _zoomLimit = 50f;

    private Transform _myTransform;

    private Camera _cam;

    private void Awake()
    {
        _myTransform = transform;
        _cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (_targets.Count == 0) 
            return;

        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + _offSet;

        _myTransform.position = Vector3.SmoothDamp(_myTransform.position, newPosition, ref _velocity, _smoothTime);

        float newZoom = Mathf.Lerp(_maxZoom, _minZoom, GetGreatedDistance() / _zoomLimit);
        _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, newZoom, Time.deltaTime);
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

    private float GetGreatedDistance()
    {
        var bounds = new Bounds(_targets[0].position, Vector3.zero);

        for (int i =  0; i < _targets.Count; ++i)
        {
            bounds.Encapsulate(_targets[i].position);
        }

        return bounds.size.x;
    }
}
