using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueScene _dialogue;

    public event Action<DialogueScene> Scene;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer.Equals(Constants.Layer.PLAYER) &&
            collider.gameObject.name.Equals(Constants.ObjectName.PLAYER))
        {
            Scene(_dialogue);
        }
    }
}
