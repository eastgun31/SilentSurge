using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public enum EnemyState //wjr 상태머신
    {
        patrolling, hear, findtarget
    }

    public EnemyState state;

    NavMeshAgent m_enemy;
    [SerializeField] Vector3[] customDestinations; // 적 캐릭터가 이동할 목적지들의 좌표를 저장합니다.
    int m_currentDestinationIndex = 0;
    Transform m_player;
    bool m_followingPlayer = false; // 적 캐릭터가 플레이어를 추적 중인지 여부를 나타냅니다.

    

    public bool m_triggered = false; // 트리거 충돌 여부를 나타냅니다.
    public Vector3 targetpos;

    int dLevel = 1;

    [SerializeField] private float stoppingDistance = 1f; // 적이 멈출 거리

    public event Action<Player> PlayerSpotted; // 플레이어 발견 시 이벤트
    bool noactiving;

    // 체력 관련 변수
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    //적 수정_김동건
    Sight sight;
    [SerializeField]
    private int indexcount;

    void Start()
    {
        state = EnemyState.patrolling;
        sight = GetComponent<Sight>(); 
        noactiving = true;
        m_enemy = GetComponent<NavMeshAgent>();
        m_enemy.stoppingDistance = 0; // 적의 멈출 거리 설정
        m_enemy.avoidancePriority = 50; // 벽을 피하기 위한 우선순위 설정
        //m_player = GameObject.FindGameObjectWithTag("Player").transform;
        //SetNextDestination();

        // 초기 체력 설정
        currentHealth = maxHealth;
        indexcount = 0;
    }

    void Update()
    {
        if (sight.findT)
        {
            state = EnemyState.findtarget;
            Debug.Log("플레이어발견");
            if(!m_followingPlayer)
                NeviClear();
            m_enemy.SetDestination(sight.detectTarget.position); // 플레이어를 발견하면 플레이어를 따라갑니다.

            //// 플레이어를 발견했을 때 이벤트 발생
            //PlayerSpotted?.Invoke(m_player.GetComponent<Player>());
        }
        else if(state == EnemyState.findtarget && !sight.findT)
            state = EnemyState.patrolling;
        else if (!sight.findT && state == EnemyState.hear)
        {
            m_enemy.stoppingDistance = 0f;
            ChasePlayer(targetpos); // 트리거 충돌 시 플레이어를 따라갑니다.
        }
        else if(!m_triggered && !sight.findT)
        {
            m_followingPlayer = false;
            m_enemy.isStopped = false;
            //if (!m_enemy.pathPending && m_enemy.remainingDistance < stoppingDistance)
            //{
            //    SetNextDestination(); // 평상시에는 기본적인 목적지로 이동합니다.
            //}
            if(customDestinations.Length > 0 )
                EnemyPatrol();
        }
        else if(state == EnemyState.patrolling)
        {
            EnemyPatrol();
        }
    }
    public void ChasePlayer(Vector3 position)
    {
        m_enemy.SetDestination(position);
        if(noactiving)
            StartCoroutine(ChasePlayerRoutine(position, 5f)); // 대기 시간 5초 
    }

    IEnumerator ChasePlayerRoutine(Vector3 position, float chaseDuration)
    {
        noactiving = false;
        yield return new WaitForSeconds(chaseDuration);
        m_triggered = false; // 트리거 충돌 상태 종료
        noactiving = true;
        m_enemy.stoppingDistance = stoppingDistance;
        //SetNextDestination();
        state = EnemyState.patrolling;
    }

    void NeviClear()    //적 네비 초기화
    {
        m_followingPlayer = true;
        m_enemy.isStopped = true;
        m_enemy.velocity = Vector3.zero;
        m_enemy.stoppingDistance = stoppingDistance;
        m_enemy.isStopped = false;
        indexcount = 0;
    }

    void EnemyPatrol()  //적순찰
    {
        if(Vector3.Distance(transform.position, customDestinations[indexcount]) > 1f)
        {
            m_enemy.SetDestination(customDestinations[indexcount]);
            //indexcount = (indexcount + 1) % customDestinations.Length;
        }
        else if (Vector3.Distance(transform.position, customDestinations[indexcount]) <= 1f)
        {
            indexcount++;
            if(indexcount == customDestinations.Length)
                indexcount = 0;
            m_enemy.SetDestination(customDestinations[indexcount]);
        }
    }

    void SetNextDestination()
    {
        if (!sight.findT && !m_followingPlayer && customDestinations.Length > 0)
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

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Sound"))
    //    {
    //        m_triggered = true; // 트리거 충돌 시 상태를 변경합니다.
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 총알 등과의 충돌이면서 플레이어 총알인 경우에만 피해를 받습니다.
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
            TakeDamage(10); // 10의 데미지를 입습니다.
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            m_enemy.isStopped = true;
            m_enemy.velocity = Vector3.zero;
            Die(); // 체력이 0 이하가 되면 사망 처리합니다.
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject); // 적 오브젝트를 파괴합니다.
    }
}
