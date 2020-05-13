using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float _strength;

    public event Action<Vector3> ChangeCursorPosition;
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

        if (hitObj.collider != null)
        {
            if (Input.GetMouseButtonDown(1) && hitObj.collider.gameObject.layer.Equals(Constants.Layer.SOIL)) {
                Debug.Log("Summon vines!");
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
            ChangeCursorPosition(hit.point);
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
