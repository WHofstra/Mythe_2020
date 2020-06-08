using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PunchableObjectScript : MonoBehaviour
{
    Rigidbody rb;
    EnemyHealth enemyHealth;
    NavMeshAgent agent;

    bool inAir = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Punch(Vector3 force)
    {
        StartCoroutine(PunchCoroutine(force, 10, 0.4333f));
    }

    public void VineHit(Vector3 force)
    {
        if (enemyHealth != null)
        {
            if (inAir)
            {
                enemyHealth.Hit(10);
            }

            inAir = false;
            agent.enabled = false;
            //Debug.Log("Stopped");
            StartCoroutine(InAirBoolChange());
        }
        rb.drag = 0;
        rb.AddForce(force, ForceMode.Impulse);
    }

    IEnumerator InAirBoolChange()
    {
        yield return new WaitForSeconds(1f);
        rb.velocity = new Vector3(0, 0, 0);
        inAir = true;
    }

    IEnumerator PunchCoroutine(Vector3 aForce, int damage, float secs)
    {
        yield return new WaitForSeconds(secs);

        if (enemyHealth != null) {
            enemyHealth.Hit(damage);
        }
        rb.AddForce(aForce, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider col)
    {
        if (agent != null && inAir)
        {
            rb.drag = 1;
            agent.Warp(transform.position);
            agent.enabled = true;
        }
    }
}
