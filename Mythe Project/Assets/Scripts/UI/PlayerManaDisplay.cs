using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaDisplay : PlayerStatDisplay
{
    protected PlayerMana playerMana;
    protected Image bar;

    protected override void Awake()
    {
        playerMana = FindObjectOfType<PlayerMana>();
        bar = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        base.Awake();

        if (playerMana != null)
        {
            playerMana = playerMana.GetComponent<PlayerMana>();
            playerMana.ChangeAmount += SetAmount;
        }
    }

    void SetAmount()
    {
        float percentage = ((float)playerMana.Mana / (float)playerMana.MaximumAmount);
        text.text = playerMana.Mana.ToString();
        bar.fillAmount = percentage;
    }
}
