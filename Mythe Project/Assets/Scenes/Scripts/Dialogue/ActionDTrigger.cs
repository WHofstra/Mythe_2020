using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDTrigger : DialogueTrigger
{
    protected enum ActionKind {
        EXISTING, MOVING, JUMPING, RUNNING
    }

    [SerializeField] protected ActionKind _action;
    [SerializeField] protected float _secondsToWait;

    protected override void Start()
    {
        base.Start();

        if (_action == ActionKind.EXISTING) {
            StartCoroutine(WaitTillEnd(totalDuration));
        }
    }

    protected override float GetSecondsToWait()
    {
        return _secondsToWait;
    }

    IEnumerator WaitTillEnd(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        TriggerNextSceneTrigger();
    }
}
