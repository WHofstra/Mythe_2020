﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    //[SerializeField] float _rotationSpeed;
    //[SerializeField] float _offsetMagnitude;

    RectTransform canvas;
    //RectTransform arrow;
    DialogueSystem system;
    Image image;
    Text text;
    Text monologuingName;

    //Vector3 arrowOrigin;
    //Vector3 arrowOffset;

    void Awake()
    {
        canvas = transform.parent.GetComponent<RectTransform>();
        system = FindObjectOfType<DialogueSystem>();
        //arrow = transform.GetChild(0).GetComponent<RectTransform>();
        image = transform.GetChild(1).GetComponent<Image>();
        text = transform.GetChild(2).GetComponent<Text>();

        if (transform.GetChild(2).GetChild(0) != null) {
            monologuingName = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        }

        if (system != null)
        {
            system = system.GetComponent<DialogueSystem>();
            system.ChangeImage += ChangeImage;
            system.EndImage += ImageEnd;
            system.ChangeText += ChangeText;
            system.ChangeName += ChangeName;
        }

        /*
        arrowOrigin = new Vector3(0, 0, 0);
        arrowOffset = new Vector3(600, 400, 0);
        arrowOffset = (arrowOffset / arrowOffset.magnitude) * _offsetMagnitude;//*/
    }

    void ChangeImage(Sprite anImage)
    {
        image.sprite = anImage;
    }

    void ImageEnd()
    {
        image.enabled = false;
    }

    void ChangeText(string aString)
    {
        text.text = aString;
    }

    void ChangeName(string aString)
    {
        monologuingName.text = aString;
    }

    /*
    void SetArrowPositionAndRotation(float hor)
    {
        arrowOffset = Quaternion.AngleAxis(hor * _rotationSpeed, Vector3.forward) * arrowOffset;
        arrow.anchoredPosition = arrowOrigin + arrowOffset;
        arrow.rotation = Quaternion.Euler(0, 0, -90 +
        (Mathf.Atan2((arrow.anchoredPosition.y - arrowOrigin.y), (arrow.anchoredPosition.x - arrowOrigin.x)) * (180 / Mathf.PI)));
    }//*/

    /*
    void EnableArrow(bool enable)
    {
        arrow.gameObject.SetActive(enable);
    }//*/
}
