using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ManaProperties : MonoBehaviour
{
    [SerializeField] int _value;
    [SerializeField] bool _setAsTemporary;
    [SerializeField] float _magneticForce;

    PlayerMana mana;
    Rigidbody rb;
    BoxCollider objCol;
    MeshRenderer rendr;
    Component halo;
    PropertyInfo haloProp;

    //To Create a Blinking Effect
    bool meshVisible;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objCol = GetComponent<BoxCollider>();
        rendr = GetComponent<MeshRenderer>();

        meshVisible = true;
        rb.useGravity = _setAsTemporary;

        if (_setAsTemporary)
        {
            halo = GetComponent(Constants.ComponentNames.HALO);
            haloProp = halo.GetType().GetProperty(Constants.ComponentNames.ENABLED);

            StartCoroutine(LeaveForSeconds(4.0f));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.Equals(Constants.Layer.PLAYER) &&
            collision.gameObject.name.Equals(Constants.ObjectName.PLAYER))
        {
            if ((mana != null) && (mana.Mana < mana.MaximumAmount))
            {
                mana.AddMana(_value);
                Destroy(gameObject);
            }
            else if ((mana != null) && (mana.Mana >= mana.MaximumAmount))
            {
                SetColliderAndGravityTo(false);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer.Equals(Constants.Layer.PLAYER) &&
            col.name.Equals(Constants.ObjectName.PLAYER))
        {
            mana = col.gameObject.GetComponent<PlayerMana>();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer.Equals(Constants.Layer.PLAYER) &&
            col.name.Equals(Constants.ObjectName.PLAYER))
        {
            if ((mana != null) && (mana.Mana < mana.MaximumAmount)) {
                transform.position = Vector3.Slerp(transform.position, col.transform.position, _magneticForce * Time.deltaTime);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer.Equals(Constants.Layer.PLAYER) &&
            col.name.Equals(Constants.ObjectName.PLAYER))
        {
            SetColliderAndGravityTo(true);
        }
    }

    void SetColliderAndGravityTo(bool active)
    {
        objCol.isTrigger = !active;

        if (_setAsTemporary) {
            rb.useGravity = active;
        }
    }

    IEnumerator LeaveForSeconds(float secs)
    {
        yield return new WaitForSeconds(secs);
        StartCoroutine(SwitchAfterSeconds(0.1f));
        StartCoroutine(DisappearAfterSeconds(3.0f));
    }

    IEnumerator SwitchAfterSeconds(float secs)
    {
        yield return new WaitForSeconds(secs);

        haloProp.SetValue(halo, !meshVisible);
        rendr.enabled = !meshVisible;
        meshVisible   = !meshVisible;

        StartCoroutine(SwitchAfterSeconds(0.1f));
    }

    IEnumerator DisappearAfterSeconds(float secs)
    {
        yield return new WaitForSeconds(secs);
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
