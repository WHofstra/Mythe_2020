using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "In-game Dialogue")]
public class InGameDialogue : DialogueScene
{
    [SerializeField] protected bool _playOnLoop;

    [SerializeField] protected float _repeatAfterSeconds;

    public override bool GetLoop()
    {
        return _playOnLoop;
    }

    public override float GetRepeatInSeconds()
    {
        return _repeatAfterSeconds;
    }
}
