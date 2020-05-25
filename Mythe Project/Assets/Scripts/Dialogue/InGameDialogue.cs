using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "In-game Dialogue")]
public class InGameDialogue : DialogueScene
{
    [SerializeField] protected bool _playOnLoop;
    public bool PlayOnLoop { get { return _playOnLoop; } }

    [SerializeField] protected float _repeatAfterSeconds;
    public float RepeatSec { get { return _repeatAfterSeconds; } }
}
