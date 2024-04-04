using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    NavMeshAgent m_enemy;
    [SerializeField] Vector3[] customDestinations; // 이 배열은 적 캐릭터가 이동할 목적지들의 좌표를 저장합니다.
    int m_currentDestinationIndex = 0;
    Transform m_player;
    bool m_followingPlayer = false; // 이 변수는 적 캐릭터가 플레이어를 추적 중인지 여부를 나타냅니다.
    int dLevel = 1;

    public float stoppingDistance = 2f; // 적이 멈출 거리

    public event Action<Player> PlayerSpotted; // 플레이어 발견 시 이벤트

    // 체력 관련 변수
    [SerializeField] private int maxHealth = 100;

    

    private int currentHealth;

    void Start()
    {
        m_enemy = GetComponent<NavMeshAgent>();
        m_enemy.stoppingDistance = stoppingDistance; // 적의 멈출 거리 설정
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        SetNextDestination();

        // 초기 체력 설정
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            if (!m_followingPlayer)
            {
                m_followingPlayer = true;
                m_enemy.SetDestination(m_player.position);

                // 플레이어를 발견했을 때 이벤트 발생
                PlayerSpotted?.Invoke(m_player.GetComponent<Player>());
            
            }
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

            //if (dLevel == 2 || dLevel == 3)
            //{
            //    // 레벨 2 또는 레벨 3인 경우 추가 동작 수행
            //    // 예를 들어, 특정 좌표로 이동하도록 설정할 수 있습니다.
            //}
           if (dLevel == 1)
            {
                // 기본적인 목적지 설정
                m_enemy.SetDestination(customDestinations[m_currentDestinationIndex]);
                m_currentDestinationIndex = (m_currentDestinationIndex + 1) % customDestinations.Length;
            }
        }
    }

    bool CanSeePlayer() // 적 캐릭터가 플레이어를 시야에 볼 수 있는지 여부를 판단하고 반환.
    {
        if (m_player == null)
            return false;

        Vector3 directionToPlayer = m_player.position - transform.position; // 플레이어와 적 사이의 방향을 계산
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, Mathf.Infinity)) // 적과 플레이어 사이의 장애물 여부를 확인
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
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

        // 총알 등과의 충돌이면서 플레이어 총알인 경우에만 피해를 받음
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10); // 10의 데미지를 입음
        }
    }

    // 피해를 입는 메서드
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die(); // 체력이 0 이하가 되면 사망 처리
        }
    }

    // 사망 처리 메서드
    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject); // 적 오브젝트 파괴
    }
}
