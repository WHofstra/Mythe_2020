using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : AnimatorScript
{
    protected void Start()
    {
        anim = GetComponent<Animator>();
        
        if (anim != null) {
            //Debug.Log("Animator found.");
        }
    }
}
