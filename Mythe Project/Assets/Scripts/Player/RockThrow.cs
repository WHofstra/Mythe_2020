using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RockThrow : MonoBehaviour
{
    public event Action lifts;

    [SerializeField] LayerMask layer;
    [SerializeField] PlayerAttack _player;

    PlayerAnimator playerAnim;
    Collider[] hitColliders;

    bool lifting;

    void Start()
    {
        playerAnim = GetComponent<PlayerAnimator>();
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

                //playerAnim.Play(Constants.AnimatorTriggerString.LIFT); //Used for Previous Models and Animator
                playerAnim.Play(Constants.AnimatorTriggerString.THROW_ROCK);

                //Debug.Log("lift");
                StartCoroutine(SetLiftingState(lifting, 0.4f));
            }
            else if (Input.GetMouseButtonDown(1) && lifting)
            {
                playerAnim.Play(Constants.AnimatorTriggerString.THROW_ROCK);
                StartCoroutine(SetLiftingState(lifting, 0.4f));
            }

            if (lifts != null)
            {
                if (Input.GetMouseButtonDown(1) && lifting)
                {
                    GetRayFront(Constants.Layer.PLAYER);
                    StartCoroutine(Shoot(0.4f));
                }
                else if (!Input.GetMouseButtonDown(1))
                {
                    StartCoroutine(Lift(0.4f));
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
            hitColliders[i].gameObject.GetComponent<RockBehavior>().AddToAction(GetComponent<RockThrow>());
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

    IEnumerator SetLiftingState(bool state, float secs)
    {
        yield return new WaitForSeconds(secs);

        if (state) {
            lifting = false;
        } else {
            lifting = true;
        }
    }

    IEnumerator Lift(float secs)
    {
        yield return new WaitForSeconds(secs);
        lifts?.Invoke();
    }

    IEnumerator Shoot(float secs)
    {
        yield return new WaitForSeconds(secs);

        for (int i = 0; i < hitColliders.Length; i++) {
            hitColliders[i].gameObject.GetComponent<RockBehavior>().Shoot(GetRayFront(Constants.Layer.PLAYER).point);
        }

        hitColliders = null;
        lifts = null;
    }
}
