using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Constants.States currentState;

    //List of objects that need to access the States
    [SerializeField]
    private List<GameObject> StateChangers = new List<GameObject>();

    PlayerHealth playerHealth;
    GameOverScript gameOver;
    PauseButton pauseButton;
    PauseMenu pauseMenu;

    void Start()
    {
        playerHealth = StateChangers[0].GetComponent<PlayerHealth>();
        gameOver = StateChangers[1].GetComponent<GameOverScript>();
        pauseButton = GetComponent<PauseButton>();
        pauseMenu = FindObjectOfType<PauseMenu>();

        // Add Delegates that can change the state;
        if (playerHealth != null) {
            playerHealth.Die += ChangeState;
        }

        if (pauseButton != null) {
            pauseButton.PauseGame += Pause;
            ChangeState(Constants.States.PLAYING);
        }

        if (pauseMenu != null) {
            pauseMenu = pauseMenu.GetComponent<PauseMenu>();
            pauseMenu.Resume += Pause;
        }
    }

    void Pause()
    {
        if (currentState == Constants.States.PLAYING || currentState == Constants.States.PAUSE)
        {
            CheckPauseState(currentState);
        }
    }

    void CheckPauseState(Constants.States state)
    {
        if (state == Constants.States.PAUSE) {
            Time.timeScale = 1;
            ChangeMouseProperties(false);
            ChangeState(Constants.States.PLAYING);
        }
        else {
            Time.timeScale = 0;
            ChangeMouseProperties(true);
            ChangeState(Constants.States.PAUSE);
        }
    }

    void ChangeMouseProperties(bool setTo)
    {
        if (setTo) {
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
        }
        Cursor.visible = setTo;
    }

    public void ChangeState(Constants.States state) 
    {
        // Changes Current State
        currentState = state;
        //Debug.Log(currentState);
        StateChanged();
    }

    public void StateChanged()
    {
        // Add functions that check the Current State
        gameOver.GameOver(currentState);
    }
}
