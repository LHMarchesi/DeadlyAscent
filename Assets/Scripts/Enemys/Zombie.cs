using System;
using UnityEngine;

public enum States
{
    idle, attacking, walkingAround, followingPlayer
}

public class Zombie : MonoBehaviour
{
    [SerializeField] private Enemy enemyData;
    [SerializeField] private float visionRange;
    [SerializeField] private float attackRange;

    private Rigidbody rb;
    private PlayerController player;
    [SerializeField] private States currentState;
    private float attackCooldownTimer = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        InitializeEnemy();
    }

    private void Update()
    {
        HandleStates();
    }
    private void FixedUpdate()
    {
        if (currentState == States.followingPlayer)
        {
            MoveTowardsPlayer();
        }
    }

    private void HandleStates()
    {
        switch (currentState)
        {
            case States.idle:
                if (isPlayerOnRange())
                {
                    currentState = States.followingPlayer;
                }
                break;

            case States.attacking:
                Attack();
                break;

            case States.walkingAround:
                if (!isPlayerOnRange())
                {
                    HandlePatrol();
                }
                break;

            case States.followingPlayer:
                
                break;

            default:
                break;
        }
    }

    private void InitializeEnemy()
    {
        currentState = States.idle;
    }

    private bool isPlayerOnRange()
    {
        float sqrDist = (transform.position - player.transform.position).sqrMagnitude;
        float sqrVisionRange = visionRange * visionRange;

        // Check if within vision range
        if (sqrDist < sqrVisionRange)
        {
            return true;
        }
        else
        {
            currentState = States.idle;
            return false;
        }
    }

    private void MoveTowardsPlayer()
    {
        float sqrDist = (transform.position - player.transform.position).sqrMagnitude;
        float sqrAttackRange = attackRange * attackRange;

        // If within attack range, stop and attack
        if (sqrDist <= sqrAttackRange)
        {
            currentState = States.attacking;
        }
        else if (isPlayerOnRange()) // Otherwise,  if isPlayerOnRange move towards the player
        {
            Vector3 nextPosition = Vector3.MoveTowards(transform.position, player.transform.position, enemyData.Speed * Time.deltaTime);
            rb.MovePosition(nextPosition);
        }
    }

    private void HandlePatrol()
    {

    }

    private void Attack()
    {
        attackCooldownTimer -= Time.deltaTime;

        if (attackCooldownTimer <= 0)
        {
            Debug.Log($"{enemyData.Name} attacks the player!");
            attackCooldownTimer = 1f / enemyData.AttackSpeed;
            currentState = States.followingPlayer;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Color para el rango de visión
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        // Color para el rango de ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
