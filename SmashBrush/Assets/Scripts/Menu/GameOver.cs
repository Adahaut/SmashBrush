using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI _txt;

    private void Start()
    {
        _txt.text = "Player " + PlayerPrefs.GetString("Player") + " WIN";
    }
}
