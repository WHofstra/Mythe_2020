using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RockThrow : MonoBehaviour
{
    public event Action lifts;
    [SerializeField]
    LayerMask layer;
    [SerializeField]
    Transform enemy;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            LiftRocks();
            Debug.Log("lift");
        }
        if (lifts != null)
        {
            lifts();
            if (Input.GetMouseButtonDown(1))
            {

            }
        }
    }
    public void LiftRocks()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10,layer);
        for(int i = 0; i < hitColliders.Length; i++)
        {
                Debug.Log(hitColliders[i].name);
                hitColliders[i].gameObject.GetComponent<RockBehavior>().AddToAction(this.GetComponent<RockThrow>());
        }
    }
}
