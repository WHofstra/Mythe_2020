using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineCollisions : MonoBehaviour
{
    [SerializeField] float _strength;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer.Equals(Constants.Layer.ENEMY) &&
            collider.GetComponent<PunchableObjectScript>() != null)
        {
            float degToRad = Mathf.PI / 180;
            collider.gameObject.GetComponent<PunchableObjectScript>().Punch
            (new Vector3(Mathf.Sin(transform.parent.transform.rotation.eulerAngles.y * degToRad), 0.25f,
                        Mathf.Cos(transform.parent.transform.rotation.eulerAngles.y * degToRad)) * _strength);
        }
    }
}
