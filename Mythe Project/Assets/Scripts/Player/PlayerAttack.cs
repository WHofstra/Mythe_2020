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
    public event Action<string> ChangeWeapon;
    public event Action TurnCursorOff;

    PlayerMana mana;
    Animator anim;
    Constants.SecondaryWeapon currentWeapon;

    Dictionary<Constants.SecondaryWeapon, string> weaponNames = new Dictionary<Constants.SecondaryWeapon, string>();
    int weaponAmount;

    public PlayerMana Mana { get { return mana; } }
    public Constants.SecondaryWeapon CurrentWeapon { get { return currentWeapon; } }

    void Start()
    {
        Cursor.visible = false;
        mana = transform.parent.GetComponent<PlayerMana>();
        anim = transform.GetChild(0).GetComponent<Animator>();

        currentWeapon = Constants.SecondaryWeapon.VINES;
        weaponAmount = Enum.GetNames(typeof(Constants.SecondaryWeapon)).Length;
        weaponNames[Constants.SecondaryWeapon.VINES] = "Vegetable Overgrowth";
        weaponNames[Constants.SecondaryWeapon.ROCKS] = "Terrakinesis";
        ChangeWeapon(weaponNames[currentWeapon]);
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
                currentWeapon == Constants.SecondaryWeapon.VINES)
            {
                anim.SetTrigger(Constants.AnimatorTriggerString.THROW_ROCK);

                if ((mana == null) || (mana != null && mana.GetAttackPossibility(currentWeapon))) {
                    StartCoroutine(AttackCoroutine(hitObj, 0.4f));
                }
            }
        }

        if (Input.GetAxis(Constants.InputString.WEAPON_SWITCH) > 0) {
            currentWeapon = ScrollWeaponWheel((int)currentWeapon, false);
            ChangeWeapon(weaponNames[currentWeapon]);
        }
        else if (Input.GetAxis(Constants.InputString.WEAPON_SWITCH) < 0) {
            currentWeapon = ScrollWeaponWheel((int)currentWeapon, true);
            ChangeWeapon(weaponNames[currentWeapon]);
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

    Constants.SecondaryWeapon ScrollWeaponWheel(int aWeapon, bool scrollingUp)
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

        //Debug.Log(((SecondaryWeapon)aWeapon).ToString());
        return (Constants.SecondaryWeapon)aWeapon;
    }

    IEnumerator AttackCoroutine(RaycastHit hit, float secs)
    {
        yield return new WaitForSeconds(secs);
        VineAttack(hit);
    }
}
