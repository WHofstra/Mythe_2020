using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScene : ScriptableObject
{
    [SerializeField] protected string[] _sentences;
    public string[] Sentences { get { return _sentences; } }

    [SerializeField] protected bool _typeOutSentence;
    public bool TypeOut { get { return _typeOutSentence; } }

    [SerializeField] protected float[] _sentenceDuration;
    public float[] SentDuration { get { return _sentenceDuration; } }
}
