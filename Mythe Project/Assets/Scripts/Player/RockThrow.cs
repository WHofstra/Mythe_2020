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
    Collider[] hitColliders;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            LiftRocks();
            Debug.Log("lift");
            gameObject.GetComponent<PlayerAnimator>().Play(Constants.AnimatorTriggerString.LIFT);
        }
        if (lifts != null)
        {
            
            if (Input.GetMouseButtonDown(1))
            {
                gameObject.GetComponent<PlayerAnimator>().Play(Constants.AnimatorTriggerString.THROW_ROCK);
                GetRayFront(Constants.Layer.PLAYER);
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    hitColliders[i].gameObject.GetComponent<RockBehavior>().Shoot(GetRayFront(Constants.Layer.PLAYER).point);
                }
                hitColliders = null;
                lifts = null;
            }
            else if(lifts != null)
            {
               lifts();
            }
        }
    }
    public void LiftRocks()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 10,layer);
        for(int i = 0; i < hitColliders.Length; i++)
        {
                Debug.Log(hitColliders[i].name);
                hitColliders[i].gameObject.GetComponent<RockBehavior>().AddToAction(this.GetComponent<RockThrow>());
        }
    }
    RaycastHit GetRayFront(int layer)
    {
        RaycastHit hit;
        int layerMask = 1 << layer;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.GetChild(0).forward, out hit, 30000, layerMask))
        {
            Debug.DrawRay(transform.position, transform.GetChild(0).forward * hit.distance, Color.green);
            Debug.Log("HIT " + hit.collider.gameObject.name);
        }
        return hit;
    }
}
