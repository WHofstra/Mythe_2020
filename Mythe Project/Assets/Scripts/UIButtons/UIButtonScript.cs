using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonScript : MonoBehaviour
{
    public void CreateNewGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    protected void CheckChild(int aPart, bool setActive)
    {
        transform.GetChild(aPart).gameObject.SetActive(setActive);
    }
}
