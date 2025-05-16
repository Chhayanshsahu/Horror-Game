using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public enum AISTATE { PATROL, CHASE, ATTACK };

    [Header("References")]
    public Transform player;
    public NavMeshAgent enemy;

    [Header("Settings")]
    public AISTATE enemyState = AISTATE.PATROL;
    public float patrolStopDistance = 2f;
    public float attackRange = 1.5f;
    public float chaseExitRange = 15f;
    public float attackCooldown = 1f;
    public float rotationSpeed = 5f;

    [Header("Waypoints")]
    public List<Transform> waypoints = new List<Transform>();

    private Transform currentWaypoint;
    private bool canAttack = true;
    private bool isPlayerInTrigger = false;

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj) player = playerObj.transform;
            else Debug.LogError("Player not found!");
        }

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

    private IEnumerator PatrolState()
    {
        while (enemyState == AISTATE.PATROL)
        {
            if (currentWaypoint != null)
            {
                enemy.SetDestination(currentWaypoint.position);

                if (Vector3.Distance(transform.position, currentWaypoint.position) < patrolStopDistance)
                {
                    currentWaypoint = waypoints[Random.Range(0, waypoints.Count)];
                }
            }
            yield return null;
        }
    }

    private IEnumerator ChaseState()
    {
        while (enemyState == AISTATE.CHASE)
        {
            if (player == null)
            {
                ChangeState(AISTATE.PATROL);
                yield break;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Check if player escaped
            if (!isPlayerInTrigger && distanceToPlayer > chaseExitRange)
            {
                ChangeState(AISTATE.PATROL);
                yield break;
            }

            // Check attack condition
            if (distanceToPlayer < attackRange)
            {
                ChangeState(AISTATE.ATTACK);
                yield break;
            }

            // Continue chasing
            enemy.SetDestination(player.position);
            yield return null;
        }
    }

    private IEnumerator AttackState()
    {
        enemy.SetDestination(transform.position);

        while (enemyState == AISTATE.ATTACK)
        {
            if (player == null)
            {
                ChangeState(AISTATE.PATROL);
                yield break;
            }

            // Face player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,
                rotationSpeed * Time.deltaTime);

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRange)
            {
                ChangeState(AISTATE.CHASE);
                yield break;
            }

            if (canAttack)
            {
                Debug.Log("Attacking Player!");
                canAttack = false;
                yield return new WaitForSeconds(attackCooldown);
                canAttack = true;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            if (enemyState != AISTATE.CHASE && enemyState != AISTATE.ATTACK)
            {
                ChangeState(AISTATE.CHASE);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}