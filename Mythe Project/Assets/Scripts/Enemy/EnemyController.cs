using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    int followDistance = 30;

    PlayerHealth ph;
    PlayerAnimator playerAnim;
    EnemyAnimator enemyAnim;
    NavMeshAgent agent;

    bool attackable = true;
    bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ph = player.GetComponent<PlayerHealth>();
        playerAnim = player.GetComponent<PlayerAnimator>();
        enemyAnim = transform.GetChild(0).GetComponent<EnemyAnimator>();
    }

    void Update()
    {
        if (agent != null && agent.enabled)
        {
            agent.SetDestination(player.transform.position);
            DistanceChecker(player.transform.position, transform.position);
        }
    }
    private void DistanceChecker(Vector3 player, Vector3 enemy)
    {
        if(Vector3.Distance(player, enemy) < followDistance)
        {
            agent.isStopped = false;
            CheckForVelocity(agent.velocity.magnitude);
        }
        else
        {
            //Debug.Log("too far");
            StopMoving(true);
        }

        if(Vector3.Distance(player,enemy) < 4f)
        {
            isAttacking = true;
            StopMoving(true);
            //Debug.Log("Touch");
            if (attackable)
            {
                StartCoroutine(Attack());
            }
        }
    }

    void StopMoving(bool setTo)
    {
        agent.isStopped = setTo;
        enemyAnim.SetBoolTo(Constants.AnimatorTriggerString.WALKING, !setTo);
    }

    void CheckForVelocity(float velocity)
    {
        if (velocity > 0) {
            enemyAnim.SetBoolTo(Constants.AnimatorTriggerString.WALKING, true);
        }
        else {
            enemyAnim.SetBoolTo(Constants.AnimatorTriggerString.WALKING, false);
        }
    }

    IEnumerator Attack()
    {
        attackable = false;
        yield return new WaitForSeconds(1f);

        enemyAnim.Play(Constants.AnimatorTriggerString.PUNCH);
        StartCoroutine(DamagePlayer());
    }

    IEnumerator DamagePlayer()
    {
        yield return new WaitForSeconds(0.2666f);
        if (playerAnim != null && isAttacking &&
           (Vector3.Distance(player.transform.position, transform.position) < 4f))
        {
            ph.GetHit(10);
            playerAnim.Play(Constants.AnimatorTriggerString.HIT);
        }
        attackable = true;
    }
}
