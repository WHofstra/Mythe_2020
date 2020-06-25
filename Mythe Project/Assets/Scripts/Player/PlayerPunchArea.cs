using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class PlayerPunchArea : MonoBehaviour
{
    /*
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
    }*/
    [SerializeField] float _strength;
    [SerializeField]
    TargetEnemy targeter;
    
    void Start()
    {
        targeter = GetComponent<TargetEnemy>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && targeter != null)
        {
            GameObject target = targeter.GetTarget();
            if (target != null)
            {
                transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                Vector3 dir = target.transform.position - transform.position;
                dir.Normalize();
                transform.position = target.transform.position - dir * 3;
                target.GetComponent<PunchableObjectScript>().Punch(dir * _strength);
            }
        }
    }
}
