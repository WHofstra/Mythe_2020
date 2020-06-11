using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    [SerializeField] protected Animator anim;

    virtual public void Play(string animation)
    {
        anim.SetTrigger(animation);
    }

    virtual public void SetBoolTo(string animation, bool state)
    {
        anim.SetBool(animation, state);
    }
}
