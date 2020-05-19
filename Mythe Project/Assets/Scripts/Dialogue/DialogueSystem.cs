using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueSystem : MonoBehaviour
{
    public event Action<Sprite> ChangeImage;
    public event Action<string> ChangeText;

    public void AddStringToScene(string aSentence)
    {
        
    }

    public void AddImageToScene(string aSentence)
    {

    }
}
