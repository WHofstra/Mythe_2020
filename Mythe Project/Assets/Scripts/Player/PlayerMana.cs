using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] int _maximumManaAmount;

    public event Action ChangeAmount;

    Dictionary<Constants.SecondaryWeapon, int> manaRequired = new Dictionary<Constants.SecondaryWeapon, int>();
    int currentMana;

    public int MaximumAmount { get { return _maximumManaAmount; } }
    public int Mana { get { return currentMana; } }

    void Awake()
    {
        currentMana = _maximumManaAmount;
        manaRequired[Constants.SecondaryWeapon.VINES] = 10;
        manaRequired[Constants.SecondaryWeapon.ROCKS] = 25;
        ChangeAmount();
    }

    public bool GetAttackPossibility(Constants.SecondaryWeapon aWeapon)
    {
        if (currentMana >= manaRequired[aWeapon]) {
            return true;
        }

        return false;
    }

    public void SubtractMana(Constants.SecondaryWeapon aWeapon)
    {
        if (currentMana >= manaRequired[aWeapon]) {
            currentMana -= manaRequired[aWeapon];
        }
        else {
            currentMana = 0; //Precautions, No Negative Integers
        }
        ChangeAmount();
    }

    public void AddMana(int aValue)
    {
        if (currentMana <= (_maximumManaAmount - aValue)) {
            currentMana += aValue;
        }
        else {
            currentMana = _maximumManaAmount;
        }
        ChangeAmount();
    }
}
