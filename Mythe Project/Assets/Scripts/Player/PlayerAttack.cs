using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float _hitDistance;
    [SerializeField] float _strength;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        MouseInput(GetRayFront(Constants.Layer.PLAYER), _hitDistance);
    }

    void MouseInput(RaycastHit hitObj, float distance)
    {
        if (hitObj.collider != null)
        {
            if (Input.GetMouseButtonDown(0) && hitObj.distance <= distance){
                //Debug.Log("You're punching " + hitObj.collider.name + ".");

                if (hitObj.collider.gameObject.GetComponent<PunchableObjectScript>() != null)
                {
                    float degToRad = Mathf.PI / 180;
                    hitObj.collider.gameObject.GetComponent<PunchableObjectScript>().Punch(
                    new Vector3(Mathf.Sin(transform.parent.transform.rotation.eulerAngles.y * degToRad), 0.25f,
                                Mathf.Cos(transform.parent.transform.rotation.eulerAngles.y * degToRad)) * 10 * _strength);
                }
            }
            else if (Input.GetMouseButtonDown(0)) {
                //Debug.Log("You're too far away from " + hitObj.collider.name + " to hit it.");
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
