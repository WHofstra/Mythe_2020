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

    public enum SecondaryWeapon
    {
        VINES, ROCKS
    }

    Animator anim;
    SecondaryWeapon currentWeapon;

    int weaponAmount;

    public SecondaryWeapon CurrentWeapon { get { return currentWeapon; } }

    void Start()
    {
        Cursor.visible = false;
        anim = transform.GetChild(0).GetComponent<Animator>();
        currentWeapon = SecondaryWeapon.ROCKS;
        weaponAmount = Enum.GetNames(typeof(SecondaryWeapon)).Length;
    }

    void Update()
    {
        MouseInput(GetRayFront(Constants.Layer.SOIL));
    }

    void MouseInput(RaycastHit hitObj)
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Punching Animation
            anim.SetTrigger(Constants.AnimatorTriggerString.PUNCH);
        }

        if (hitObj.collider != null && hitObj.distance <= _maximumDistance)
        {
            //Attack with Vines
            if (Input.GetMouseButtonDown(1) && hitObj.collider.gameObject.layer.Equals(Constants.Layer.SOIL) &&
                currentWeapon == SecondaryWeapon.VINES) {
                VineAttack(hitObj);
            }
        }

        if (Input.GetAxis(Constants.InputString.WEAPON_SWITCH) > 0) {
            currentWeapon = ScrollWeaponWheel((int)currentWeapon, false);
            //Debug.Log(currentWeapon.ToString());
        }
        else if (Input.GetAxis(Constants.InputString.WEAPON_SWITCH) < 0) {
            currentWeapon = ScrollWeaponWheel((int)currentWeapon, true);
            //Debug.Log(currentWeapon.ToString());
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

    SecondaryWeapon ScrollWeaponWheel(int aWeapon, bool scrollingUp)
    {
        if (scrollingUp)
        {
            if (aWeapon < weaponAmount - 1) {
                aWeapon++;
            } else {
                aWeapon = 0;
            }
        }
        else
        {
            if (aWeapon > 0) {
                aWeapon--;
            } else {
                aWeapon = weaponAmount - 1;
            }
        }

        Debug.Log(((SecondaryWeapon)aWeapon).ToString());
        return (SecondaryWeapon)aWeapon;
    }
}
