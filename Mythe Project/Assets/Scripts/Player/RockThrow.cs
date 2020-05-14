using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RockThrow : MonoBehaviour
{
    public event Action lifts;

    [SerializeField] LayerMask layer;
    [SerializeField] PlayerAttack _player;

    Collider[] hitColliders;

    bool lifting;

    void Start()
    {
        lifting = false;
    }

    void Update()
    {
        CheckInput(_player.CurrentWeapon);
    }

    void CheckInput(PlayerAttack.SecondaryWeapon aWeapon)
    {
        if (aWeapon == PlayerAttack.SecondaryWeapon.ROCKS)
        {
            if (Input.GetMouseButtonDown(1) && !lifting)
            {
                LiftRocks();
                //Debug.Log("lift");
                StartCoroutine(SetLiftingState(lifting));
            }
            else if (Input.GetMouseButtonDown(1) && lifting)
            {
                StartCoroutine(SetLiftingState(lifting));
            }

            if (lifts != null)
            {
                if (Input.GetMouseButtonDown(1) && lifting)
                {
                    GetRayFront(Constants.Layer.PLAYER);
                    for (int i = 0; i < hitColliders.Length; i++)
                    {
                        hitColliders[i].gameObject.GetComponent<RockBehavior>().Shoot(GetRayFront(Constants.Layer.PLAYER).point);
                    }
                    hitColliders = null;
                    lifts = null;
                }
                else if (!Input.GetMouseButtonDown(1))
                {
                    lifts();
                }
            }
        }
    }

    public void LiftRocks()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 10,layer);
        for(int i = 0; i < hitColliders.Length; i++)
        {
            //Debug.Log(hitColliders[i].name);
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
            //Debug.Log("HIT " + hit.collider.gameObject.name);
        }
        return hit;
    }

    IEnumerator SetLiftingState(bool state)
    {
        yield return new WaitForSeconds(0.5f);

        if (state) {
            lifting = false;
        } else {
            lifting = true;
        }
    }
}
