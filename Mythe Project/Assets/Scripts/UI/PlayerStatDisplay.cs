using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatDisplay : MonoBehaviour
{
    [SerializeField] protected Sprite[] _sprites;

    protected Text text;
    protected Image image;

    virtual protected void Awake()
    {
        text = transform.GetChild(1).GetComponent<Text>();
        image = transform.GetChild(0).GetComponent<Image>();
    }
}
