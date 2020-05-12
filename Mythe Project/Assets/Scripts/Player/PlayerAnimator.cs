using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    public void Play(string animation)
    {
        anim.SetTrigger(animation);
    }
}
