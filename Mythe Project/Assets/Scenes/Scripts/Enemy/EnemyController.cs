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
    NavMeshAgent agent;

    bool attackable = true;
    bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ph = player.GetComponent<PlayerHealth>();
        playerAnim = player.GetComponent<PlayerAnimator>();
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
            agent.Resume();
            isAttacking = false;
        }
        else
        {
            //Debug.Log("too far");
            agent.Stop();
        }

        if(Vector3.Distance(player,enemy) < 4f)
        {
            isAttacking = true;
            agent.Stop();
            //Debug.Log("Touch");
            if (attackable)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        attackable = false;
        yield return new WaitForSeconds(1f);
        if (playerAnim != null && isAttacking)
        {
            ph.GetHit(10);
            playerAnim.Play(Constants.AnimatorTriggerString.HIT);
        }
        attackable = true;
    }
}
