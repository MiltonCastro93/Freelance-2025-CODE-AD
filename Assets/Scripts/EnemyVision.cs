using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVision : MonoBehaviour
{
    [Header("Detección")]
    public Transform player;
    public LayerMask obstacleMask;
    public float viewDistance = 10f;
    public float viewAngle = 90f;

    [Header("Patrullaje")]
    public List<Transform> patrolPoints = new List<Transform>();
    public float patrolSpeed = 2f;
    public float waitTimeAtPoint = 1f;
    private int currentPatrolIndex = 0;
    private float waitTimer = 0f;
    private bool waitingAtPoint = false;

    [Header("Persecución")]
    public float chaseSpeed = 4f;
    private bool isChasing = false;
    private float lostSightTimer = 0f;
    public float lostSightDuration = 3f;

    [Header("Comunicación entre enemigos")]
    public float alertRadius = 10f;

    [Header("Visión visible (cono/linterna)")]
    public Light visionCone;
    public Color patrolColor = new Color(1f, 1f, 0.6f); 
    public Color chaseColor = Color.red;
    public bool coneVisible = true;

    private NavMeshAgent agent;
    private EnemyAnimaiton anim;

    void Start()
    {
        anim = GetComponentInChildren<EnemyAnimaiton>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;

        if (patrolPoints.Count > 0)
            agent.SetDestination(patrolPoints[0].position);

        if (visionCone != null)
        {
            visionCone.spotAngle = viewAngle;
            visionCone.range = viewDistance;
            visionCone.color = patrolColor;
            visionCone.enabled = coneVisible;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            DetectPlayer();
            Patrol();
        }

        Vector3 fixedPos = transform.position;
        fixedPos.y = 0.5f;
        transform.position = fixedPos;

        anim.MoveEnemi(agent.velocity.sqrMagnitude);
    }

    void DetectPlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (angle < viewAngle / 2f && distanceToPlayer < viewDistance)
        {
            if (!Physics.Raycast(transform.position, dirToPlayer, distanceToPlayer, obstacleMask))
            {
                PlayerController pc = player.GetComponent<PlayerController>();
                if (pc != null && !pc.isHidden)
                {
                    isChasing = true;
                    agent.speed = chaseSpeed;
                    AlertNearbyEnemies();

                    if (visionCone != null)
                        visionCone.color = chaseColor;
                }
            }
        }
    }

    void ChasePlayer()
    {
        if (player == null) return;

        agent.SetDestination(player.position);
        Vector3 dir = (player.position - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * 5f);

        if (!CanSeePlayer())
        {
            lostSightTimer += Time.deltaTime;
            if (lostSightTimer >= lostSightDuration)
            {
                isChasing = false;
                lostSightTimer = 0f;
                agent.speed = patrolSpeed;

                if (patrolPoints.Count > 0)
                    agent.SetDestination(patrolPoints[currentPatrolIndex].position);

                if (visionCone != null)
                    visionCone.color = patrolColor;
            }
        }
        else
        {
            lostSightTimer = 0f;
        }
    }

    bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (angle < viewAngle / 2f && distanceToPlayer < viewDistance)
        {
            if (!Physics.Raycast(transform.position, dirToPlayer, distanceToPlayer, obstacleMask))
            {
                PlayerController pc = player.GetComponent<PlayerController>();
                return pc != null && !pc.isHidden;
            }
        }
        return false;
    }

    void AlertNearbyEnemies()
    {
        Collider[] nearby = Physics.OverlapSphere(transform.position, alertRadius);
        foreach (Collider col in nearby)
        {
            EnemyVision other = col.GetComponent<EnemyVision>();
            if (other != null && other != this)
            {
                other.OnAlertReceived(player.position);
            }
        }
    }

    public void OnAlertReceived(Vector3 lastKnownPosition)
    {
        if (!isChasing)
        {
            isChasing = true;
            agent.speed = chaseSpeed;
            agent.SetDestination(lastKnownPosition);

            if (visionCone != null)
                visionCone.color = chaseColor;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Count == 0) return;

        if (waitingAtPoint)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                waitingAtPoint = false;
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            }
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.3f)
        {
            waitingAtPoint = true;
            waitTimer = waitTimeAtPoint;
        }

        Vector3 direction = agent.steeringTarget - transform.position;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, direction.normalized, Time.deltaTime * 5f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewDistance);

        Gizmos.color = new Color(1, 0.5f, 0f, 0.2f);
        Gizmos.DrawWireSphere(transform.position, alertRadius);
    }
}












