using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private List<Material> _skins = new List<Material>();
    [SerializeField] private GameObject _fist, _foot;
    [SerializeField] private List<Color> _lineColors = new List<Color>();
    private Material _currentSkin = null;
    private Color _currentLineColor = Color.white;

    private void Start()
    {
        AssignSkin();
        _fist.GetComponent<TrailRenderer>().enabled = false;
        _foot.GetComponent<TrailRenderer>().enabled = false;
    }

    public void AssignSkin()
    {
        if (_currentSkin == null)
        {
            if (_skins.Count > 0)
            {
                //Debug.Log(Camera.main.GetComponent<CameraMovement>()._nbPlayer - 1);
                _currentSkin = _skins[Camera.main.GetComponent<CameraMovement>()._nbPlayer - 1];
                _currentLineColor = _lineColors[Camera.main.GetComponent<CameraMovement>()._nbPlayer - 1];
            }
            gameObject.GetComponent<MeshRenderer>().material = _currentSkin;
            _fist.GetComponent<MeshRenderer>().material = _currentSkin;
            _foot.GetComponent<MeshRenderer>().material = _currentSkin;

            _fist.GetComponent<TrailRenderer>().startColor = _currentLineColor;
            _foot.GetComponent<TrailRenderer>().startColor = _currentLineColor;
        }
    }
}
