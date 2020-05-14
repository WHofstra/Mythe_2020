using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatDisplay : UIscript
{
    protected PlayerHealth playerHealth;

    protected new void Start()
    {
        base.Start();

        playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth = playerHealth.GetComponent<PlayerHealth>();
            playerHealth.ChangeHealth += GetHealthChange;
        }
    }

    void GetHealthChange()
    {
        text.text = playerHealth.CurrentHealth.ToString();
    }
}
