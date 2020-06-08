using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public enum MenuPart
    { TITLE_SCREEN, LEVEL_SELECT}

    MenuPart currentPart;
    string aSent;

    void Start()
    {
        currentPart = MenuPart.TITLE_SCREEN;

        CheckChild(MenuPart.LEVEL_SELECT, false);
        CheckChild(currentPart, true);
    }

    void CheckChild(MenuPart aPart, bool setActive)
    {
        transform.GetChild((int)aPart).gameObject.SetActive(setActive);
    }

    public void CreateNewGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeMenuTo(string aPart)
    {
        int part = (int)aPart[0] - 48;

        if (part < MenuPart.GetNames(typeof(MenuPart)).Length)
        {
            CheckChild(currentPart, false);
            currentPart = (MenuPart)part;
            CheckChild(currentPart, true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
