using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private List<Material> _skins = new List<Material>();
    private Material _currentSkin = null;

    private void Start()
    {
        AssignSkin();
    }

    public void AssignSkin()
    {
        if (_currentSkin == null)
        {
            if (_skins.Count > 0)
            {
                _currentSkin = _skins[Camera.main.GetComponent<CameraMovement>()._nbPlayer - 1];
            }
            gameObject.GetComponent<MeshRenderer>().material = _currentSkin;
        }
    }
}
