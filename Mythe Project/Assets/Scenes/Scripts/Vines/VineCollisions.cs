using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineCollisions : MonoBehaviour
{
    [SerializeField] float _strength;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<PunchableObjectScript>() != null)
        {
            collider.gameObject.GetComponent<PunchableObjectScript>().VineHit
            (transform.up * _strength);
        }
    }
}
