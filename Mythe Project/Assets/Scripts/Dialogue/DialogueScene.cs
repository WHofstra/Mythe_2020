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

    public virtual bool GetLoop()
    {
        return false;
    }

    public virtual float GetRepeatInSeconds()
    {
        return 0f;
    }

    public virtual string[] GetNames()
    {
        return null;
    }

    public float GetDuration(int durationArrayLength, int sentArrayLength, float total)
    {
        if (durationArrayLength >= (sentArrayLength - 1))
        {
            for (int i = 0; i < durationArrayLength; i++) {
                total += _sentenceDuration[i];
            }
        }
        else
        {
            for (int i = 0; i < sentArrayLength; i++) {
                total += _sentenceDuration[0];
            }
        }

        return total;
    }
}
