using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehavior : MonoBehaviour
{
    Rigidbody rb;
    EnemyHealth enemy;

    Vector3 startPos;
    float timer = 0;
    bool shot;

    public bool Shot { get { return shot; } }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        shot = false;
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
        shot = true;
        //Debug.Log("Shoot");
        Vector3 aim = transform.position - target;
        //Debug.Log(target);
        aim.Normalize();
        rb.AddForce(-aim*15, ForceMode.Impulse);
        rb.AddForce(Vector3.up*4, ForceMode.Impulse);
    }
    
    public void OnTriggerEnter(Collider col)
    {
        enemy = col.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.Hit(rb.velocity.magnitude);
        }

        if (col.gameObject.layer.Equals(Constants.Layer.SOIL)) {
            shot = false;
        }
    }
}
