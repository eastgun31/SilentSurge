using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent m_enemy;
    [SerializeField] Vector3[] customDestinations;
    int m_currentDestinationIndex = 0;
    Transform m_player;
    bool m_followingPlayer = false;

    // 적 캐릭터의 시야각과 시야 거리
    public float viewAngle = 1190f;
    public float viewDistance = 1110f;

    void Start()
    {
        m_enemy = GetComponent<NavMeshAgent>();
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        SetNextDestination();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            m_followingPlayer = true;
            m_enemy.SetDestination(m_player.position);
        }
        else
        {
            m_followingPlayer = false;
            if (!m_enemy.pathPending && m_enemy.remainingDistance < 0.5f)
            {
                SetNextDestination();
            }
        }
    }

    void SetNextDestination()
    {
        if (!m_followingPlayer && customDestinations.Length > 0)
        {
            m_enemy.isStopped = false;
            m_enemy.SetDestination(customDestinations[m_currentDestinationIndex]);
            m_currentDestinationIndex = (m_currentDestinationIndex + 1) % customDestinations.Length;
        }
    }

    bool CanSeePlayer()
    {
        if (m_player == null)
            return false;

        Vector3 directionToPlayer = m_player.position - transform.position;
        if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2f)
        {
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, viewDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 장애물을 감지하면 목적지를 다시 설정
            SetNextDestination();
        }
    }
}

