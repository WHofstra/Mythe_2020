using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] protected DialogueScene _dialogue;
    public DialogueScene Dialogue { get { return _dialogue; } }

    public event Action<DialogueScene> Scene;
    public event Action NextTrigger;

    protected virtual void Start()
    {
        Scene(_dialogue);
        //Checking if this is the only GameObject that's active...
        //Debug.Log(gameObject.name + " started its sequence.");
    }

    protected void ChangeScene(DialogueScene aScene)
    {
        Scene(aScene);
    }

    protected void TriggerNextSceneTrigger()
    {
        NextTrigger();
    }

    IEnumerator RepetitionCoroutine()
    {
        yield return new WaitForSeconds(_dialogue.GetRepeatInSeconds());
    }
}
