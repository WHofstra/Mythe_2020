using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] InGameDialogue[] _dialogue;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer.Equals(Constants.Layer.PLAYER))
        {

        }
    }
}
