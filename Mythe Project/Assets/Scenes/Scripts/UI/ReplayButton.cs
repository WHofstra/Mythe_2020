using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : MonoBehaviour
{
    public void Replay()
    {
        Screen.lockCursor = true;
        Time.timeScale = 1;
        Cursor.visible = false;
        Application.LoadLevel(1);
    }
}
