using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum AISTATE { PATROL, CHASE, ATTACK }

    [Header("References")]
    public Transform player;
    public NavMeshAgent enemy;
    public Animator enemyAnimator;

    [Header("Animation Names")]
    public string walkAnimation = "Walk";
    public string runAnimation = "Run";
    public string attackAnimation = "Attack";

    [Header("Movement Settings")]
    public float patrolSpeed = 3.5f;
    public float chaseSpeed = 7f;
    public float attackRange = 1.5f;
    public float chaseExitRange = 15f;
    public float attackCooldown = 1f;
    public float rotationSpeed = 5f;

    [Header("Waypoints")]
    public List<Transform> waypoints = new List<Transform>();

    private AISTATE currentState;
    private Transform currentWaypoint;
    private bool canAttack = true;

    void Start()
    {
        InitializeEnemy();
        StartPatrol();
    }

    void InitializeEnemy()
    {
        if (waypoints.Count > 0)
        {
            currentWaypoint = waypoints[Random.Range(0, waypoints.Count)];
            enemy.SetDestination(currentWaypoint.position);
        }
        enemy.autoBraking = false;
    }

    void Update()
    {
        HandleStateBehavior();
    }

    void HandleStateBehavior()
    {
        switch (currentState)
        {
            case AISTATE.PATROL:
                UpdatePatrol();
                break;
            case AISTATE.CHASE:
                UpdateChase();
                break;
            case AISTATE.ATTACK:
                UpdateAttack(); // Added missing method call
                break;
        }
    }

    // Added missing UpdateAttack method
    void UpdateAttack()
    {
        if (player == null) return;

        // Face the player while attacking
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Check if player moved out of range
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            StartChase();
        }
    }

    void StartPatrol()
    {
        currentState = AISTATE.PATROL;
        enemy.speed = patrolSpeed;
        enemy.isStopped = false;
        enemyAnimator.Play(walkAnimation);

        if (waypoints.Count > 0)
        {
            currentWaypoint = waypoints[Random.Range(0, waypoints.Count)];
            enemy.SetDestination(currentWaypoint.position);
        }
    }

    void UpdatePatrol()
    {
        if (waypoints.Count == 0) return;

        if (enemy.remainingDistance < 1f)
        {
            currentWaypoint = waypoints[Random.Range(0, waypoints.Count)];
            enemy.SetDestination(currentWaypoint.position);
        }
    }

    void StartChase()
    {
        currentState = AISTATE.CHASE;
        enemy.speed = chaseSpeed;
        enemy.isStopped = false;
        enemyAnimator.Play(runAnimation);
    }

    void UpdateChase()
    {
        if (player == null) return;

        enemy.SetDestination(player.position);

        if (Vector3.Distance(transform.position, player.position) > chaseExitRange)
        {
            StartPatrol();
        }
        else if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            StartAttack();
        }
    }

    void StartAttack()
    {
        currentState = AISTATE.ATTACK;
        enemy.isStopped = true;
        enemyAnimator.Play(attackAnimation);

        if (canAttack)
        {
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        canAttack = false;

        // Wait for attack animation duration
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
        enemy.isStopped = false;

        // Check if should resume chase
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            StartChase();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentState != AISTATE.ATTACK)
        {
            StartChase();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && currentState == AISTATE.CHASE)
        {
            StartPatrol();
        }
    }
}
