using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatDisplay : MonoBehaviour
{
    Text text;
    PlayerHealth playerHealth;
    RectTransform rTransform;

    void Start()
    {
        text = GetComponent<Text>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        rTransform = GetComponent<RectTransform>();

        //rTransform.transform.position = new Vector3(rTransform.sizeDelta.x / 2 + 40, rTransform.sizeDelta.y, 0);

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
