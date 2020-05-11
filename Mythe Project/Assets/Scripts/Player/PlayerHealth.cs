using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _maximumHealth;

    public event Action changeHealth;

    int currentHealth;
    bool hit;
    
    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    void Start()
    {
        currentHealth = _maximumHealth;
        hit = false;
        changeHealth();
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer.Equals(Constants.Layer.HAZARD))
        {
            GetHit(9);
            hit = true;
            StartCoroutine(HitCoroutine());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer.Equals(Constants.Layer.HAZARD))
        {
            hit = false;
        }
    }

    IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        hit = false;
    }

    void GetHit(int damage)
    {
        if (!hit){
            if (currentHealth > damage) {
                currentHealth -= damage;
            }
            else {
                currentHealth = 0;
            }
            changeHealth();
        }
    }
}
