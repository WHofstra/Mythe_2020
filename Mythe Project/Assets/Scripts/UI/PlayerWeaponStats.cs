using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponStats : UIscript
{
    protected PlayerAttack attack;

    void Awake()
    {
        base.Start();

        attack = FindObjectOfType<PlayerAttack>();
        if (attack != null)
        {
            attack = attack.GetComponent<PlayerAttack>();
            attack.ChangeWeapon += ChangeText;
        }
    }

    void ChangeText(string ability)
    {
        text.text = "Ability\t\t\n[" + ability + "]";
    }
}
