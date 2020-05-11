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
            agent.Stop();
            Debug.Log("Touch");
            ph.GetHit(1);
        }
        else
        {
            agent.Resume();
        }
    }
}
