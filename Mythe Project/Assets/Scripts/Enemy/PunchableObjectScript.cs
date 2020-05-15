using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PunchableObjectScript : MonoBehaviour
{
    Rigidbody rb;
    bool inAir = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Punch(Vector3 force)
    {
        if (gameObject.GetComponent<EnemyHealth>() != null)
        {
            gameObject.GetComponent<EnemyHealth>().Hit(10);
        }
        rb.AddForce(force, ForceMode.Impulse);
    }
    public void VineHit(Vector3 force)
    {
        if (gameObject.GetComponent<EnemyHealth>() != null && inAir)
        {
            gameObject.GetComponent<EnemyHealth>().Hit(10);
        }
        if (gameObject.GetComponent<NavMeshAgent>() != null)
        {
            inAir = false;
            GetComponent<NavMeshAgent>().enabled = false;
            //Debug.Log("Stopped");
            StartCoroutine(InAirBoolChange());
        }
        rb.drag = 0;
        rb.AddForce(force, ForceMode.Impulse);
    }

    IEnumerator InAirBoolChange()
    {
        yield return new WaitForSeconds(1f);
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        inAir = true;
    }

    void OnTriggerEnter(Collider col)
    {

        if (gameObject.GetComponent<NavMeshAgent>() != null && inAir)
        {
            rb.drag = 1;
            GetComponent<NavMeshAgent>().Warp(transform.position);
            GetComponent<NavMeshAgent>().enabled = true;

        }
    }
}
