using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum AISTATE { PATROL, CHASE, ATTACK };
    public Transform player;
    public NavMeshAgent enemy;
    public AISTATE enemyState = AISTATE.PATROL;
    public float distanceOffset = 2f;

    public List<Transform> waypoints = new List<Transform>();
    private Transform currentWaypoint;

    private void Start()
    {
        if (waypoints.Count > 0)
        {
            currentWaypoint = waypoints[Random.Range(0, waypoints.Count)];
            ChangeState(AISTATE.PATROL);
        }
        else
        {
            Debug.LogWarning("No waypoints assigned to enemy!");
        }
    }

    public void ChangeState(AISTATE newState)
    {
        StopAllCoroutines();
        enemyState = newState;

        switch (enemyState)
        {
            case AISTATE.PATROL:
                StartCoroutine(PatrolState());
                break;
            case AISTATE.CHASE:
                StartCoroutine(ChaseState());
                break;
            case AISTATE.ATTACK:
                StartCoroutine(AttackState());
                break;
        }
    }

    public IEnumerator PatrolState()
    {
        while (enemyState == AISTATE.PATROL)
        {
            enemy.SetDestination(currentWaypoint.position);

            if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceOffset)
            {
                currentWaypoint = waypoints[Random.Range(0, waypoints.Count)];
            }

            yield return null;
        }
    }

    public IEnumerator ChaseState()
    {
        while (enemyState == AISTATE.CHASE)
        {
            if (Vector3.Distance(transform.position, player.position) < distanceOffset)
            {
                ChangeState(AISTATE.ATTACK);
                yield break;
            }

            enemy.SetDestination(player.position);
            yield return null;
        }
    }

    public IEnumerator AttackState()
    {
        while (enemyState == AISTATE.ATTACK)
        {
            if (Vector3.Distance(transform.position, player.position) > distanceOffset)
            {
                ChangeState(AISTATE.CHASE);
                yield break;
            }

            // Attack logic here
            Debug.Log("Attack");
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeState(AISTATE.CHASE);
        }
    }
}