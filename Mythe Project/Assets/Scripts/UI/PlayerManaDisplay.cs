using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManaDisplay : PlayerStatDisplay
{
    protected PlayerMana playerMana;

    protected override void Awake()
    {
        playerMana = FindObjectOfType<PlayerMana>();
        base.Awake();

        if (playerMana != null)
        {
            playerMana = playerMana.GetComponent<PlayerMana>();
            playerMana.ChangeAmount += SetAmount;
        }
    }

    void SetAmount()
    {
        text.text = playerMana.Mana.ToString();
    }
}
