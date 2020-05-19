using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    RectTransform canvas;
    DialogueSystem system;
    Image image;
    Text text;

    void Awake()
    {
        canvas = transform.parent.GetComponent<RectTransform>();
        system = FindObjectOfType<DialogueSystem>();
        image = transform.GetChild(0).GetComponent<Image>();
        text = transform.GetChild(1).GetComponent<Text>();

        if (system != null)
        {
            system = system.GetComponent<DialogueSystem>();
            system.ChangeImage += ChangeImage;
            system.ChangeText += ChangeText;
        }
    }

    void ChangeImage(Sprite anImage)
    {
        image.sprite = anImage;
    }

    void ChangeText(string aString)
    {
        text.text = aString;
    }
}
