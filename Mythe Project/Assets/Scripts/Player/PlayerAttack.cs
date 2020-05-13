using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float _strength;
    [SerializeField] float _maximumDistance;

    public event Action<RaycastHit> ChangeCursorPosition;
    public event Action<RaycastHit> VineAttack;
    public event Action TurnCursorOff;

    Animator anim;

    void Start()
    {
        Cursor.visible = false;
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        MouseInput(GetRayFront(Constants.Layer.SOIL));
    }

    void MouseInput(RaycastHit hitObj)
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger(Constants.AnimatorTriggerString.PUNCH);
        }

        if (hitObj.collider != null && hitObj.distance <= _maximumDistance)
        {
            if (Input.GetMouseButtonDown(1) && hitObj.collider.gameObject.layer.Equals(Constants.Layer.SOIL)) {
                VineAttack(hitObj);
            }
        }
    }

    RaycastHit GetRayFront(int layer)
    {
        RaycastHit hit;
        int layerMask = 1 << layer;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 30000, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);

            if (hit.distance <= _maximumDistance) {
                ChangeCursorPosition(hit);
            } else {
                TurnCursorOff();
            }
        }
        else
        {
            TurnCursorOff();

            //In Case of No Ray Drawing or No Visible Ray
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 4, Color.red);
        }

        return hit;
    }
}
