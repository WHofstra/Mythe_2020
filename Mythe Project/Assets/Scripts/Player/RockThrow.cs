using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RockThrow : MonoBehaviour
{
    public event Action lifts;
    public void LiftRocks()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10,12);
        for(int i = 0; i < hitColliders.Length; i++)
        {
            hitColliders[i].gameObject.GetComponent<RockBehavior>().AddToAction(this.GetComponent<RockThrow>());
        }
    }
}
