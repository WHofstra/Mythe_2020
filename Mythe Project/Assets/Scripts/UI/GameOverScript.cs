using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    GameObject screen;

    void Start()
    {
        screen.SetActive(false);
    }
    public void GameOver(Constants.States state)
    {
        if(state == Constants.States.GAME_OVER)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            Cursor.visible = true;
            screen.SetActive(true);
        }
    }
}
