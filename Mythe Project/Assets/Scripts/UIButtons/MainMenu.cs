using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UIButtonScript
{
    public event Action ResetCredits;

    public enum MenuPart
    { TITLE_SCREEN, LEVEL_SELECT, CREDITS }

    protected MenuPart currentPart;

    protected void Start()
    {
        currentPart = MenuPart.TITLE_SCREEN;
        CheckChild((int)MenuPart.LEVEL_SELECT, false);
        CheckChild((int)MenuPart.CREDITS, false);
        CheckChild((int)currentPart, true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
    }

    public void ChangeMenuTo(string aPart)
    {
        int part = (int)aPart[0] - 48;

        if (part < MenuPart.GetNames(typeof(MenuPart)).Length)
        {
            CheckChild((int)currentPart, false);
            currentPart = (MenuPart)part;
            CheckChild((int)currentPart, true);
        }
    }

    public void ResetCreditsPosition()
    {
        ResetCredits();
    }
}
