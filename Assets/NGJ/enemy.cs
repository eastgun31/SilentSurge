using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    NavMeshAgent m_enemy;
    [SerializeField] Vector3[] customDestinations;
    int m_currentDestinationIndex = 0;
    Transform m_player;
    bool m_followingPlayer = false;
    public int dLevel=0;

    public event Action<Player> PlayerSpotted; // 플레이어 발견 시 이벤트

    // 체력 관련 변수
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        m_enemy = GetComponent<NavMeshAgent>();
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
                Debug.Log("11");
            }
        }
        else
        {
            m_followingPlayer = false;
            Debug.Log("22");
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

            if (dLevel == 2 || dLevel == 3)
            {
                // 레벨 2 또는 레벨 3인 경우 추가 동작 수행
                // 예를 들어, 특정 좌표로 이동하도록 설정할 수 있습니다.
            }
            else
            {
                // 기본적인 목적지 설정
                m_enemy.SetDestination(customDestinations[m_currentDestinationIndex]);
                m_currentDestinationIndex = (m_currentDestinationIndex + 1) % customDestinations.Length;
            }
        }
    }

    bool CanSeePlayer()
    {
        if (m_player == null)
            return false;

        Vector3 directionToPlayer = m_player.position - transform.position;
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, Mathf.Infinity))
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

//using UnityEngine;
//using UnityEngine.AI;

//public class Enemy : MonoBehaviour
//{
//    NavMeshAgent m_enemy;
//    [SerializeField] Vector3[] customDestinations;
//    int m_currentDestinationIndex = 0;
//    Transform m_player;
//    bool m_followingPlayer = false;

//    // 적 캐릭터의 시야각과 시야 거리
//    public float viewAngle = 1190f;
//    public float viewDistance = 1110f;

//    // 체력 관련 변수
//    [SerializeField] private int maxHealth = 100;
//    private int currentHealth;

//    void Start()
//    {
//        m_enemy = GetComponent<NavMeshAgent>();
//        m_player = GameObject.FindGameObjectWithTag("Player").transform;
//        SetNextDestination();

//        // 초기 체력 설정
//        currentHealth = maxHealth;
//    }

//    void Update()
//    {
//        if (CanSeePlayer())
//        {
//            m_followingPlayer = true;
//            m_enemy.SetDestination(m_player.position);
//        }
//        else
//        {
//            m_followingPlayer = false;
//            if (!m_enemy.pathPending && m_enemy.remainingDistance < 0.5f)
//            {
//                SetNextDestination();
//            }
//        }
//    }

//    void SetNextDestination()
//    {
//        if (!m_followingPlayer && customDestinations.Length > 0)
//        {
//            m_enemy.isStopped = false;
//            m_enemy.SetDestination(customDestinations[m_currentDestinationIndex]);
//            m_currentDestinationIndex = (m_currentDestinationIndex + 1) % customDestinations.Length;
//        }
//    }

//    bool CanSeePlayer()
//    {
//        if (m_player == null)
//            return false;

//        Vector3 directionToPlayer = m_player.position - transform.position;
//        if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2f)
//        {
//            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, viewDistance))
//            {
//                if (hit.collider.CompareTag("Player"))
//                {
//                    return true;
//                }
//            }
//        }

//        return false;
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Obstacle"))
//        {
//            // 장애물을 감지하면 목적지를 다시 설정
//            SetNextDestination();
//        }

//        // 플레이어가 던진 물체와 충돌한 경우
//        if (collision.gameObject.CompareTag("ThrownObject"))
//        {
//            // 빙글빙글 돌기 시작
//            StartCoroutine(Spinning());
//        }
//    }

//    // 피해를 입는 메서드
//    void TakeDamage(int damage)
//    {
//        currentHealth -= damage;
//        if (currentHealth <= 0)
//        {
//            Die(); // 체력이 0 이하가 되면 사망 처리
//        }
//    }

//    // 사망 처리 메서드
//    void Die()
//    {
//        Debug.Log("Enemy died!");
//        Destroy(gameObject); // 적 오브젝트 파괴
//    }

//    // 빙글빙글 돌기 코루틴
//    IEnumerator Spinning()
//    {
//        float duration = 2f; // 빙글빙글 돌아가는 시간
//        float elapsedTime = 0f;

//        while (elapsedTime < duration)
//        {
//            transform.Rotate(Vector3.up, 360f * Time.deltaTime / duration); // 1초에 360도 회전
//            elapsedTime += Time.deltaTime;
//            yield return null;
//        }
//    }
//}


//위에 코딩은 플레이어가 던진 물체를 맞으면 제자리에서 빙글빙글 도는 코드를 추가함 (섬광탄용)


//using UnityEngine;
//using UnityEngine.AI;

//public class Enemy : MonoBehaviour
//{
//    NavMeshAgent m_enemy;
//    [SerializeField] Vector3[] customDestinations;
//    int m_currentDestinationIndex = 0;
//    Transform m_player;
//    bool m_followingPlayer = false;

//    // 적 캐릭터의 시야각과 시야 거리
//    public float viewAngle = 1190f;
//    public float viewDistance = 1110f;

//    // 체력 관련 변수
//    [SerializeField] private int maxHealth = 100;
//    private int currentHealth;

//    // 사운드 관련 변수
//    public AudioClip soundClip; // 이동할 때 재생할 사운드 클립
//    private AudioSource audioSource;

//    void Start()
//    {
//        m_enemy = GetComponent<NavMeshAgent>();
//        m_player = GameObject.FindGameObjectWithTag("Player").transform;
//        SetNextDestination();

//        // 초기 체력 설정
//        currentHealth = maxHealth;

//        // AudioSource 컴포넌트 가져오기
//        audioSource = GetComponent<AudioSource>();
//    }

//    void Update()
//    {
//        if (CanSeePlayer())
//        {
//            m_followingPlayer = true;
//            m_enemy.SetDestination(m_player.position);
//        }
//        else
//        {
//            m_followingPlayer = false;
//            if (!m_enemy.pathPending && m_enemy.remainingDistance < 0.5f)
//            {
//                SetNextDestination();
//            }
//        }
//    }

//    void SetNextDestination()
//    {
//        if (!m_followingPlayer && customDestinations.Length > 0)
//        {
//            m_enemy.isStopped = false;
//            m_enemy.SetDestination(customDestinations[m_currentDestinationIndex]);
//            m_currentDestinationIndex = (m_currentDestinationIndex + 1) % customDestinations.Length;
//        }
//    }

//    bool CanSeePlayer()
//    {
//        if (m_player == null)
//            return false;

//        Vector3 directionToPlayer = m_player.position - transform.position;
//        if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2f)
//        {
//            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, viewDistance))
//            {
//                if (hit.collider.CompareTag("Player"))
//                {
//                    return true;
//                }
//            }
//        }

//        return false;
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Obstacle"))
//        {
//            // 장애물을 감지하면 목적지를 다시 설정
//            SetNextDestination();
//        }

//        // 총알 등과의 충돌이면서 플레이어 총알인 경우에만 피해를 받음
//        if (collision.gameObject.CompareTag("Bullet"))
//        {
//            TakeDamage(10); // 10의 데미지를 입음
//        }
//    }

//    // 피해를 입는 메서드
//    void TakeDamage(int damage)
//    {
//        currentHealth -= damage;
//        if (currentHealth <= 0)
//        {
//            Die(); // 체력이 0 이하가 되면 사망 처리
//        }
//        else
//        {
//            // 피해를 입은 후 사운드 재생
//            if (soundClip != null && audioSource != null)
//            {
//                audioSource.clip = soundClip;
//                audioSource.Play();
//            }
//        }
//    }

//    // 사망 처리 메서드
//    void Die()
//    {
//        Debug.Log("Enemy died!");
//        Destroy(gameObject); // 적 오브젝트 파괴
//    }
//}


//위에 코드는 소리난 쪽으로 움직이는 코드를 추가함 
