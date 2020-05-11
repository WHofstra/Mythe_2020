using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchableObjectScript : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Punch(Vector3 force)
    {
        rb.AddForce(force);
    }
}
