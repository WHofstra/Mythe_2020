using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] protected DialogueScene _dialogue;

    public event Action<DialogueScene> Scene;

    protected bool repeat;

    private void Start()
    {
        repeat = false;
    }

    protected void ChangeScene(DialogueScene aScene)
    {
        Scene(aScene);
    }

    IEnumerator RepetitionCoroutine()
    {
        yield return new WaitForSeconds(4.0f);
    }
}
