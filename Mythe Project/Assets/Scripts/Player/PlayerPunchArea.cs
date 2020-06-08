using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunchArea : MonoBehaviour
{
    [SerializeField] float _strength;

    void OnTriggerStay(Collider collid)
    {
        if (Input.GetMouseButtonDown(0) && collid.gameObject.GetComponent<PunchableObjectScript>() != null)
        {
            float degToRad = Mathf.PI / 180;
            collid.gameObject.GetComponent<PunchableObjectScript>().Punch
            (new Vector3(Mathf.Sin(transform.parent.transform.rotation.eulerAngles.y * degToRad), 0.25f,
                        Mathf.Cos(transform.parent.transform.rotation.eulerAngles.y * degToRad)) * _strength);
        }
    }
}
