using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    int health = 50;

    public void Hit(int damage)
    {
        health -= damage;
        Debug.Log("Enemy Health is now = " + health);
        if(health <= 0)
        {
            health = 0;
            Destroy(gameObject);
        }
    }
}
