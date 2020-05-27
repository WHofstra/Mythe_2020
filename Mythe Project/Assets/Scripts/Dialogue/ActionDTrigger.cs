using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDTrigger : DialogueTrigger
{
    protected enum ActionKind {
        EXISTING, LOOKING, PUNCHING
    }

    [SerializeField] protected ActionKind _action;
    [SerializeField] protected float _secondsToWait;

    protected float totalDuration;

    protected override void Start()
    {
        totalDuration = AddDuration(_dialogue.SentDuration.Length, _dialogue.Sentences.Length, 0.0f);
        base.Start();
        StartCoroutine(WaitTillEnd());
    }

    protected float AddDuration(int durationArrayLength, int sentArrayLength, float total)
    {
        if (durationArrayLength >= (sentArrayLength - 1))
        {
            for (int i = 0; i < durationArrayLength; i++) {
                total += _dialogue.SentDuration[i];
            }
        } 
        else
        {
            for (int i = 0; i < sentArrayLength; i++) {
                total += _dialogue.SentDuration[0];
            }
        }

        total += _secondsToWait;
        return total;
    }

    IEnumerator WaitTillEnd()
    {
        yield return new WaitForSeconds(totalDuration);
        TriggerNextSceneTrigger();
    }
}
