using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseButton : MonoBehaviour
{
    [SerializeField] KeyCode[] _pauseButtons;

    public event Action PauseGame;

    void Update()
    {
        if (Input.GetKeyDown(_pauseButtons[0]) || Input.GetKeyDown(_pauseButtons[1]))
        {
            PauseGame();
        }
    }
}
