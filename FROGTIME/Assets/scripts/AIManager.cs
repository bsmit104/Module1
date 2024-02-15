//asked chatgpt for FSM examples in Unity, it recommended attack, patrol, and chase
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;  // Reference to the player's transform
    private bool isPlayerDetected = false;

    public float maxDetectionRange = 10f;
    public float chaseDistance = 5f;
    public float killDistance = 5f;
    public LayerMask obstacleLayer;

    public GameObject playerObject;

    public Transform[] patrolWaypoints;
    private int currentWaypointIndex = 0;

    public GameObject health;
    private PlayerHealth playerHealth;


    public enum AIState
    {
        Patrol,
        Chase,
        Attack
    }

    private AIState currentState = AIState.Patrol;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.Log("Player null");
        }
        SetNextWaypoint();

        playerHealth = health.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.Log("PlayerHealth null");
        }
    }

    void Update()
    {
        // Check line of sight to the player
        // if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out RaycastHit hit, maxDetectionRange, obstacleLayer))
        // {
        //     Debug.Log("hit");
        //     if (hit.collider.CompareTag("Player"))
        //     {
        //         // Player is in line of sight
        //         OnPlayerDetected();
        //     }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            OnPlayerDetected();
        }
        else
        {
            OnPlayerLost();
        }

        if (distanceToPlayer < killDistance)
        {
            killPlayer();
        }
        // }
        // else
        // {
        //     Debug.Log("no hit");
        //     // The ray hit nothing, implying the player is not in line of sight
        //     OnPlayerLost();
        // }

        // FSM
        switch (currentState)
        {
            case AIState.Patrol:
                Debug.Log("Patrol State");
                // If player is detected, transition to Chase state
                if (isPlayerDetected)
                {
                    currentState = AIState.Chase;
                    break;
                }

                // If close to the current waypoint, set the next waypoint
                if (agent.remainingDistance < 1f)
                {
                    SetNextWaypoint();
                }

                // if (Vector3.Distance(transform.position, patrolWaypoints[currentWaypointIndex].position) < 1f)
                // {
                //     SetNextWaypoint();
                // }

                // Move towards the current waypoint
                agent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
                break;

            case AIState.Chase:
                Debug.Log("Chase State");
                agent.SetDestination(player.position);

                if (Vector3.Distance(transform.position, player.position) < 2f)
                {
                    currentState = AIState.Attack;
                }
                break;

            case AIState.Attack:
                Debug.Log("Attack State");
                // not sure if gonna use
                break;

            default:
                break;
        }
    }

    private void OnPlayerDetected()
    {
        isPlayerDetected = true;
        currentState = AIState.Chase;
    }

    private void OnPlayerLost()
    {
        isPlayerDetected = false;
        currentState = AIState.Patrol;
    }

    private void killPlayer()
    {
        if (playerHealth != null)
        {
            playerObject.transform.position = new Vector3(640.9f, 14.021f, 65.394f);
            playerHealth.ChangeHealth(-50);
        }
        else
        {
            Debug.Log("PlayerHealth is null");
        }
    }

    private void SetNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        agent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
    }
}