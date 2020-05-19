using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatDisplay : MonoBehaviour
{
    [SerializeField] Sprite[] _sprites;

    PlayerHealth playerHealth;
    Text text;
    Image image;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        text = transform.GetChild(1).GetComponent<Text>();
        image = transform.GetChild(0).GetComponent<Image>();

        if (playerHealth != null)
        {
            playerHealth = playerHealth.GetComponent<PlayerHealth>();
            playerHealth.ChangeHealth += GetHealthChange;
        }
    }

    void GetHealthChange()
    {
        int spriteIndex = Mathf.RoundToInt((((float)playerHealth.CurrentHealth) / ((float)playerHealth.MaximumHealth)) * (float)(_sprites.Length - 1));
        //Debug.Log(spriteIndex);
        text.text = playerHealth.CurrentHealth.ToString();
        image.sprite = _sprites[spriteIndex];
    }
}
