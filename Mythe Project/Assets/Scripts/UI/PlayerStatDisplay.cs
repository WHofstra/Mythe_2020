using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatDisplay : MonoBehaviour
{
    Text text;
    PlayerHealth playerHealth;
    RectTransform transform;

    void Start()
    {
        text = GetComponent<Text>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        transform = GetComponent<RectTransform>();

        if (playerHealth != null)
        {
            playerHealth = playerHealth.GetComponent<PlayerHealth>();
            playerHealth.changeHealth += GetHealthChange;
        }
    }

    void GetHealthChange()
    {
        text.text = playerHealth.CurrentHealth.ToString();
    }
}
