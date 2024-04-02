using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent m_enemy;
    [SerializeField] Transform[] WayPoint;
    int m_currentWaypoint = 0;
    Transform m_player;
    bool m_followingPlayer = false;

    // 적 캐릭터의 시야각과 시야 거리
    public float viewAngle = 90f;
    public float viewDistance = 10f;

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
        if (!m_followingPlayer)
        {
            m_enemy.SetDestination(WayPoint[m_currentWaypoint].position);
            m_currentWaypoint = (m_currentWaypoint + 1) % WayPoint.Length;
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
}
