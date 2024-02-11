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

    public Transform[] patrolWaypoints;
    private int currentWaypointIndex = 0;

    public GameObject health;  // Assign the player GameObject in the Unity Editor
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
            Debug.LogError("Player not found in the scene. Make sure to tag your player GameObject.");
        }
        SetNextWaypoint();

        playerHealth = health.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component not found on the player GameObject.");
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

        // Check distance to the player
        if (distanceToPlayer < chaseDistance)
        {
            // Player is within chase distance
            OnPlayerDetected();
        }
        else
        {
            // Player is not in line of sight
            OnPlayerLost();
        }

        if (distanceToPlayer < killDistance)
        {
            // Player is within chase distance
            killPlayer();
        }
        // }
        // else
        // {
        //     Debug.Log("no hit");
        //     // The ray hit nothing, implying the player is not in line of sight
        //     OnPlayerLost();
        // }

        // FSM logic
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

                // // If close to the current waypoint, set the next waypoint
                // if (Vector3.Distance(transform.position, patrolWaypoints[currentWaypointIndex].position) < 1f)
                // {
                //     SetNextWaypoint();
                // }

                // Move towards the current waypoint
                agent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
                break;

            case AIState.Chase:
                Debug.Log("Chase State");
                // If player is detected, set the destination to the player's position
                agent.SetDestination(player.position);

                // Transition to Attack state if close enough to the player
                if (Vector3.Distance(transform.position, player.position) < 2f)
                {
                    currentState = AIState.Attack;
                }
                break;

            case AIState.Attack:
                Debug.Log("Attack State");
                // Implement your attack logic here
                break;

            default:
                break;
        }
    }

    // Call this method when the player is detected
    private void OnPlayerDetected()
    {
        isPlayerDetected = true;
        // You might want to change the state to Chase or Attack based on your FSM design
        currentState = AIState.Chase;
    }

    // Call this method when the player is lost or no longer detected
    private void OnPlayerLost()
    {
        isPlayerDetected = false;
        // You might want to change the state back to Patrol or another state based on your FSM design
        currentState = AIState.Patrol;
    }

    private void killPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.ChangeHealth(-50);
        }
        else
        {
            Debug.LogError("PlayerHealth reference is null. Make sure to set it in the Start method.");
        }
    }

    private void SetNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        agent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
    }
}