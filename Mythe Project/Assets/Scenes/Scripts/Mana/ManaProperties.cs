using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaProperties : MonoBehaviour
{
    [SerializeField] int _value;

    PlayerMana mana;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer.Equals(Constants.Layer.PLAYER) &&
            col.name.Equals(Constants.ObjectName.PLAYER))
        {
            mana = col.GetComponent<PlayerMana>();

            if ((mana != null) && (mana.Mana < mana.MaximumAmount)) {
                mana.AddMana(_value);
                Destroy(gameObject);
            }
        }
    }
}
