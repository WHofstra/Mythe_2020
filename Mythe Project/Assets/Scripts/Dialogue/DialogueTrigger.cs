using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] protected DialogueScene _dialogue;

    public event Action<DialogueScene> Scene;

    protected void ChangeScene(DialogueScene aScene)
    {
        Scene(aScene);
    }
}
