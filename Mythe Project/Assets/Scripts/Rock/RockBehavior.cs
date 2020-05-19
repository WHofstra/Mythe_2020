using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehavior : MonoBehaviour
{
    Rigidbody rb;

    Vector3 startPos;
    float timer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    void GoUp()
    {
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(startPos, startPos + Vector3.up *5, timer);
    }
    public void AddToAction(RockThrow rock)
    {
        startPos = transform.position;
        timer = 0;
        rock.lifts += GoUp;
    }
    public void Shoot(Vector3 target)
    {
        //Debug.Log("Shoot");
        Vector3 aim = transform.position - target;
        //Debug.Log(target);
        aim.Normalize();
        rb.AddForce(-aim*15, ForceMode.Impulse);
        rb.AddForce(Vector3.up*4, ForceMode.Impulse);

    }
    
    public void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<EnemyHealth>() != null)
        {
            col.GetComponent<EnemyHealth>().Hit(GetComponent<Rigidbody>().velocity.magnitude);
        }
    }
}
