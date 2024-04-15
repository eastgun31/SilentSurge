using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using ItemInfo;

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
    bool isPlayerInSight = false;  
    public GameObject bulletPrefab; // 총알의 프리팹
    public Transform bulletPos;
    public float bulletSpeed = 20f; // 총알의 발사 속도
    public GameObject gunmodal;
    public bool m_triggered = false; // 트리거 충돌 여부를 나타냅니다.
    public Vector3 targetpos;

    int maxBulletCount = 10; // 최대 총알 개수
    int currentBulletCount = 999; // 현재 총알 개수

    bool isShooting = false; // 총을 발사 중인지 여부를 나타내는 변수
  
    int dLevel = 1;

    [SerializeField] private float stoppingDistance; // 적이 멈출 거리

    public event Action<Player> PlayerSpotted; // 플레이어 발견 시 이벤트
    bool noactiving;

    // 체력 관련 변수
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    //적 수정_김동건
    Sight sight;
    [SerializeField]
    private int indexcount;
    [SerializeField]
    private bool chasing;
    public bool hearSound;
    WaitForSeconds wait;
    WaitForSeconds lvwait;
    private UnityEngine.Object bullet;
    E_CoolTime cooltime;

    void Start()
    {

        chasing = false;
        stoppingDistance = 1.5f;
        state = EnemyState.patrolling;
        sight = GetComponent<Sight>();
        noactiving = true;
        hearSound = false;
        m_enemy = GetComponent<NavMeshAgent>();
        wait = new WaitForSeconds(1f);
        lvwait = new WaitForSeconds(10f);
        StartCoroutine(EnemyStateCheck());

        m_enemy.avoidancePriority = 50; // 벽을 피하기 위한 우선순위 설정
                                        //m_player = GameObject.FindGameObjectWithTag("Player").transform;
                                        //SetNextDestination();

        // 초기 체력 설정
        currentHealth = maxHealth;
        indexcount = 0;
    }

    void Update()
    {
        if (state == EnemyState.findtarget)
        {
            TargetChase();
        }
        else if (state == EnemyState.hear)
        {
            ChaseSound(targetpos);
        }
        else if (state == EnemyState.patrolling)
        {
            EnemyPatrol();
        }
    }

    
    public void ChaseSound(Vector3 position)
    {
        //Debug.Log("소리추적");
        m_enemy.SetDestination(position);
        if (noactiving)
            StartCoroutine(ChaseSoundRoutine(position)); // 대기 시간 5초 
    }

    IEnumerator ChaseSoundRoutine(Vector3 position)
    {
        noactiving = false;
        m_enemy.stoppingDistance = 0;

        yield return new WaitForSeconds(3f);
        hearSound = false;
        noactiving = true;
        NeviClear();
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
        if (Vector3.Distance(transform.position, customDestinations[indexcount]) > 1f)
        {
            m_enemy.SetDestination(customDestinations[indexcount]);
            //indexcount = (indexcount + 1) % customDestinations.Length;
        }
        else if (Vector3.Distance(transform.position, customDestinations[indexcount]) <= 1f)
        {
            indexcount++;
            if (indexcount == customDestinations.Length)
                indexcount = 0;
            m_enemy.SetDestination(customDestinations[indexcount]);
        }
    }
    void TargetChase()
    {
        if (!chasing)
            StartCoroutine(Levelstep());
        m_enemy.stoppingDistance = stoppingDistance;
        m_enemy.SetDestination(sight.detectTarget.position);

        if (Vector3.Distance(transform.position, sight.detectTarget.position) <= 5f)
        {
            shoot(sight.detectTarget.position); // 총을 발사합니다.
        }

    }

    IEnumerator ShootRoutine()
    {
        int shotsFired = 0;
        while (shotsFired < 10)
        {
            // 총알 발사
            shoot(sight.detectTarget.position);

            // 발사 후 대기 시간
            yield return new WaitForSeconds(1f);

            shotsFired++;
        }
    }

   
    void shoot(Vector3 pos)
    {
        if (!isShooting )
        {
            isShooting = true; // 발사 중 상태로 변경
            //currentBulletCount--; // 총알 개수 감소

            m_enemy.isStopped = true;
            // 총알을 발사하는 동작을 수행합니다.
            Debug.Log("Enemy: Shooting! Remaining Bullets: " );

            GameObject bulletObject = Instantiate(bulletPrefab, gunmodal.transform.position, bulletPos.rotation);
            Rigidbody bulletRigid = bulletObject.GetComponent<Rigidbody>();
            // 총알의 방향을 적 캐릭터가 바라보는 방향으로 설정합니다.
            Vector3 shootDirection = transform.forward;

            // 총알을 생성하고 설정한 방향으로 발사합니다.
            bulletRigid.velocity = bulletPos.forward * bulletSpeed;

            // 총알 발사 후 일정 시간을 기다린 후 다음 동작으로 진행합니다.
            
            StartCoroutine(ShootDelay(0.5f)); // 1초 뒤에 다시 총 발사
        }
        //else if (currentBulletCount <= 0)
        //{
        //    Debug.Log("Out of bullets! Reloading...");
        //    StartCoroutine(ReloadDelay(3f)); // 총알을 모두 사용한 경우 3초 후에 재장전
        //}

    }
    //void ResumePatrollingAfterDelay()
    //{
    //    StartCoroutine(ResumePatrollingCoroutine(20f)); // 20초 뒤에 다시 순찰 재개
    //}

    //IEnumerator ResumePatrollingCoroutine(float delay)
    //{
    //    yield return new WaitForSeconds(20f); // 지정된 시간만큼 대기합니다.
    //    m_enemy.isStopped = false; // 순찰 재개
    //}
    //IEnumerator ReloadDelay(float delay)
    //{
    //    yield return new WaitForSeconds(5f); // 재장전 대기
    //    currentBulletCount = maxBulletCount; // 최대 총알 개수로 재장전
    //}

    IEnumerator ShootDelay(float delay)
    {
        yield return new WaitForSeconds(5f); // 지정된 시간만큼 대기합니다.
        isShooting = false; // 발사 종료 상태로 변경
    }

    

    IEnumerator EnemyStateCheck()
    {
        if (sight.findT)
        {
            GameManager.instance.playerchasing = true;
            state = EnemyState.findtarget;
        }
        else if (sight.findT && hearSound)
        {
            state = EnemyState.findtarget;
        }
        else if (!sight.findT && hearSound)
        {
            GameManager.instance.playerchasing = false;
            state = EnemyState.hear;
        }
        else if (!sight.findT && !hearSound)
        {
            GameManager.instance.playerchasing = false;
            state = EnemyState.patrolling;
        }

        yield return wait;
        StartCoroutine(EnemyStateCheck());
    }
    IEnumerator Levelstep()
    {
        chasing = true;
        yield return lvwait;

        if (state == EnemyState.findtarget && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
        {
            EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level2;
        }
        else if (state == EnemyState.findtarget && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level2)
        {
            EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;
        }
        chasing = false;
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
            Destroy(other.gameObject, 1f);
            if(indexcount != 99)
                GameManager.instance.existEnemy[indexcount] = false;
            gameObject.SetActive(false);
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    // 충돌한 물체가 벽이거나 플레이어인 경우
    //    if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
    //    {
    //        // 충돌한 총알을 몇 초 후에 삭제합니다.
    //        StartCoroutine(DestroyBulletAfterDelay(0.1f)); // 1초 후에 삭제하도록 설정
    //    }
    //}

    

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