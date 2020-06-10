using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UIButtonScript
{
    public event Action ResetCredits;

    public enum MenuPart
    { TITLE_SCREEN, LEVEL_SELECT, CREDITS }

    [SerializeField] protected Image _titleLogo;

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

        if (_titleLogo != null) {
            _titleLogo.enabled = true;
        }
    }

    public void ChangeMenuTo(string aPart)
    {
        int part = (int)aPart[0] - 48;

        if (part < MenuPart.GetNames(typeof(MenuPart)).Length)
        {
            if (_titleLogo != null && part != 2) {
                _titleLogo.enabled = true;
            }

            CheckChild((int)currentPart, false);
            currentPart = (MenuPart)part;
            CheckChild((int)currentPart, true);
        }
    }

    public void ResetCreditsPosition()
    {
        if (_titleLogo != null) {
            _titleLogo.enabled = false;
        }
        ResetCredits();
    }
}
