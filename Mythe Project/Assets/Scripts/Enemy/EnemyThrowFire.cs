using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowFire : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private float ShootTimer = 5f;

    private EnemyAnimator enemyAnim;

    private float timer = 0;
    private bool playerInRange = false;
    
    void Start()
    {
        enemyAnim = transform.GetChild(0).GetComponent<EnemyAnimator>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= ShootTimer)
        {
            timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(fireBall);
        bullet.transform.position = transform.position + new Vector3(0,2,0);
        //Debug.Log(player.transform.position);
        enemyAnim.Play(Constants.AnimatorTriggerString.PUNCH);
        bullet.GetComponent<RockBehavior>().Shoot(player.transform.position);
    }
}
