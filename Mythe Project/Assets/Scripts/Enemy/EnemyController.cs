using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    PlayerHealth ph;

    NavMeshAgent agent;

    bool attackable = true;
    bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ph = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        DistanceChecker(player.transform.position, transform.position);
    }
    private void DistanceChecker(Vector3 player, Vector3 enemy)
    {
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
        else
        {
            isAttacking = false;
            agent.Resume();
        }
    }

    IEnumerator Attack()
    {
        attackable = false;
        yield return new WaitForSeconds(1f);
        if (isAttacking)
        {
            ph.GetHit(10);
            player.GetComponent<PlayerAnimator>().Play(Constants.AnimatorTriggerString.HIT);
        }
        attackable = true;
    }
}
