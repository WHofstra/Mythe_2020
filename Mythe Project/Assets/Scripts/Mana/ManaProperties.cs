using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaProperties : MonoBehaviour
{
    [SerializeField] int _value;

    PlayerMana mana;
    Rigidbody rb;
    BoxCollider objCol;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objCol = GetComponent<BoxCollider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.Equals(Constants.Layer.PLAYER) &&
            collision.gameObject.name.Equals(Constants.ObjectName.PLAYER))
        {
            mana = collision.gameObject.GetComponent<PlayerMana>();

            if ((mana != null) && (mana.Mana < mana.MaximumAmount))
            {
                mana.AddMana(_value);
                Destroy(gameObject);
            }
            else if ((mana != null) && (mana.Mana >= mana.MaximumAmount))
            {
                rb.useGravity = false;
                objCol.enabled = false;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer.Equals(Constants.Layer.PLAYER) &&
            col.name.Equals(Constants.ObjectName.PLAYER))
        {
            objCol.enabled = true;
            rb.useGravity = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer.Equals(Constants.Layer.PLAYER) &&
            col.name.Equals(Constants.ObjectName.PLAYER))
        {
            if ((mana != null) && (mana.Mana < mana.MaximumAmount))
            {
                //Debug.Log("Gravity Active.");
                objCol.enabled = true;
                rb.useGravity = true;
            }
        }
    }
}
