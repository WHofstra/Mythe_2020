using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Constants.States currentState;

    //List of objects that need to access the States
    [SerializeField]
    private List<GameObject> StateChangers = new List<GameObject>();

    void Start()
    {
        // Add Delegates that can change the state;
        StateChangers[0].GetComponent<PlayerHealth>().Die += ChangeState; 
    }

    public void ChangeState(Constants.States state) 
    {
        // Changes Current State
        currentState = state;
        Debug.Log(currentState);
        StateChanged();
    }

    public void StateChanged()
    {
        // Add functions that check the Current State
        StateChangers[1].GetComponent<GameOverScript>().GameOver(currentState);
    }
}
