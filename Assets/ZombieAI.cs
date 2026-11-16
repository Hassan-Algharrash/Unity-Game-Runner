using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] patrolPoints;
    public Transform player;

    public float detectionRange = 12f;
    public float attackRange = 1.5f;

    private int currentPoint = 0;
    private bool chasingPlayer = false;

    public float loseSightDuration = 3f;
    private float loseSightTimer = 0f;

    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPoint].position);
        }
    }

    void Update()
    {
        float distanceToPlayer =
            Vector3.Distance(transform.position, player.position);

        // Drive animations
        anim.SetFloat("Speed", agent.velocity.magnitude);

        // Detect player
        if (distanceToPlayer < detectionRange)
        {
            chasingPlayer = true;
            loseSightTimer = 0f;
        }
        else if (chasingPlayer)
        {
            loseSightTimer += Time.deltaTime;

            if (loseSightTimer >= loseSightDuration)
            {
                chasingPlayer = false;

                if (patrolPoints.Length > 0)
                {
                    agent.SetDestination(patrolPoints[currentPoint].position);
                }
            }
        }

        // Chase player
        if (chasingPlayer)
        {
            agent.SetDestination(player.position);

            if (distanceToPlayer < attackRange)
            {
                agent.isStopped = true;

                FindFirstObjectByType<GameManager>().Fail();
            }
            else
            {
                agent.isStopped = false;
            }

            return;
        }

        // Patrol
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        currentPoint = (currentPoint + 1) % patrolPoints.Length;
        agent.SetDestination(patrolPoints[currentPoint].position);
    }
}