using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    NavMeshAgent m_enemy;
    [SerializeField] Vector3[] customDestinations; // 적 캐릭터가 이동할 목적지들의 좌표를 저장합니다.
    int m_currentDestinationIndex = 0;
    Transform m_player;
    bool m_followingPlayer = false; // 적 캐릭터가 플레이어를 추적 중인지 여부를 나타냅니다.


    public bool m_triggered = false; // 트리거 충돌 여부를 나타냅니다.
    public Vector3 soundpos;

    int dLevel = 1;

    [SerializeField] private float stoppingDistance = 1f; // 적이 멈출 거리

    public event Action<Player> PlayerSpotted; // 플레이어 발견 시 이벤트


    // 체력 관련 변수
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        m_enemy = GetComponent<NavMeshAgent>();
        m_enemy.stoppingDistance = stoppingDistance; // 적의 멈출 거리 설정
        m_enemy.avoidancePriority = 50; // 벽을 피하기 위한 우선순위 설정
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        SetNextDestination();

        // 초기 체력 설정
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            m_followingPlayer = true;
            m_enemy.SetDestination(m_player.position); // 플레이어를 발견하면 플레이어를 따라갑니다.

            // 플레이어를 발견했을 때 이벤트 발생
            PlayerSpotted?.Invoke(m_player.GetComponent<Player>());
        }
        else if (m_triggered)
        {
            m_enemy.SetDestination(soundpos); // 트리거 충돌 시 플레이어를 따라갑니다.
        }
        else
        {
            m_followingPlayer = false;

            if (!m_enemy.pathPending && m_enemy.remainingDistance < stoppingDistance)
            {
                SetNextDestination(); // 평상시에는 기본적인 목적지로 이동합니다.
            }
        }
    }
    public void ChasePlayer(Vector3 position)
    {
        m_enemy.SetDestination(position);


    }

    void SetNextDestination()
    {
        if (!m_followingPlayer && customDestinations.Length > 0)
        {
            m_enemy.isStopped = false;

            // 기본적인 목적지 설정
            m_enemy.SetDestination(customDestinations[m_currentDestinationIndex]);
            m_currentDestinationIndex = (m_currentDestinationIndex + 1) % customDestinations.Length;
        }
    }

    bool CanSeePlayer() // 적 캐릭터가 플레이어를 시야에 볼 수 있는지 여부를 판단하고 반환합니다.
    {
        if (m_player == null)
            return false;

        Vector3 directionToPlayer = m_player.position - transform.position; // 플레이어와 적 사이의 방향을 계산합니다.
        if (Vector3.Angle(transform.forward, directionToPlayer) < 90f && directionToPlayer.magnitude < stoppingDistance)
        {
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, Mathf.Infinity, ~LayerMask.GetMask("Wall"))) // 벽을 제외한 레이어에서만 충돌을 검사합니다.
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            m_triggered = true; // 트리거 충돌 시 상태를 변경합니다.
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sound"))
        {
            // 장애물을 감지하면 목적지를 다시 설정합니다.
            SetNextDestination();
        }

        // 총알 등과의 충돌이면서 플레이어 총알인 경우에만 피해를 받습니다.
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10); // 10의 데미지를 입습니다.
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die(); // 체력이 0 이하가 되면 사망 처리합니다.
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject); // 적 오브젝트를 파괴합니다.
    }
}
