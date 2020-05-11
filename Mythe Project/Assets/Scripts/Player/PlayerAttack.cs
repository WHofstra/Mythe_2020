using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float _hitDistance;

    Animator anim;

    void Start()
    {
        Cursor.visible = false;
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        MouseInput(GetRayFront(Constants.Layer.PLAYER), _hitDistance);
    }

    void MouseInput(RaycastHit hitObj, float distance)
    {
        if (hitObj.collider != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger(Constants.AnimatorTriggerString.PUNCH);
            }

            if (Input.GetMouseButtonDown(1) && hitObj.collider.gameObject.layer.Equals(Constants.Layer.SOIL)) {
                //Debug.Log("Summon vines!");
            }
        }
    }

    RaycastHit GetRayFront(int layer)
    {
        RaycastHit hit;
        int layerMask = 0 << layer;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 30000, layerMask)) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
        }

        return hit;
    }
}
