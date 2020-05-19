using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIscript : MonoBehaviour
{
    protected Text text;

    protected void Start()
    {
        text = GetComponent<Text>();
    }
}
