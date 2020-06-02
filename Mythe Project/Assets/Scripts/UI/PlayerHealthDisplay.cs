using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDisplay : PlayerStatDisplay
{
    protected PlayerHealth playerHealth;

    protected override void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        base.Awake();

        if (playerHealth != null)
        {
            playerHealth = playerHealth.GetComponent<PlayerHealth>();
            playerHealth.ChangeHealth += SetHealth;
        }
    }

    protected void SetHealth()
    {
        int spriteIndex = Mathf.RoundToInt((((float)playerHealth.CurrentHealth) / ((float)playerHealth.MaximumHealth)) * (float)(_sprites.Length - 1));
        //Debug.Log(spriteIndex);
        text.text = playerHealth.CurrentHealth.ToString();
        image.sprite = _sprites[spriteIndex];
    }
}
