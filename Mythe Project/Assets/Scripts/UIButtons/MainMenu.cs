using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UIButtonScript
{
    public enum MenuPart
    { TITLE_SCREEN, LEVEL_SELECT}

    protected MenuPart currentPart;

    protected void Start()
    {
        currentPart = MenuPart.TITLE_SCREEN;
        CheckChild((int)MenuPart.LEVEL_SELECT, false);
        CheckChild((int)currentPart, true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
}
