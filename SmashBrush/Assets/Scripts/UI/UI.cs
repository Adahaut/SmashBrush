using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    private CameraMovement _cameraMovement;
    void Start()
    {
        _cameraMovement = Camera.main.GetComponent<CameraMovement>();
    }



}
