using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    float health = 50;

    public event Action Drop;

    public void Hit(int damage)
    {
        health -= damage;
        Debug.Log("Enemy Health is now = " + health);
        if(health <= 0)
        {
            health = 0;
            Drop();
            Destroy(gameObject);
        }
    }
    public void Hit(float damage)
    {
        Hit(Mathf.RoundToInt(damage));
    }
}
