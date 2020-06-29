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

    protected float totalDuration;
    public float TotalDuration { get { return totalDuration; } }

    protected void Awake()
    {
        totalDuration = GetSecondsToWait() +
        _dialogue.GetDuration(_dialogue.SentDuration.Length, _dialogue.Sentences.Length, 0.0f);
    }

    protected virtual void Start()
    {
        //Checking if this is the only GameObject that's active...
        //Debug.Log(gameObject.name + " started its sequence.");

        Scene(_dialogue);

        if (_dialogue.GetLoop()) {
            StartCoroutine(RepetitionCoroutine(_dialogue.GetRepeatInSeconds() + totalDuration));
        }
    }

    protected void ChangeScene(DialogueScene aScene)
    {
        Scene(aScene);
    }

    protected void TriggerNextSceneTrigger()
    {
        NextTrigger();
    }

    protected virtual float GetSecondsToWait()
    {
        return 0;
    }

    IEnumerator RepetitionCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Scene(_dialogue);
        StartCoroutine(RepetitionCoroutine(seconds));
    }
}
