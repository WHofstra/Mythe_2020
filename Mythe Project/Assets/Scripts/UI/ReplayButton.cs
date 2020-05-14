using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : MonoBehaviour
{
    public void Replay()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        Application.LoadLevel("SampleScene");
    }
}
