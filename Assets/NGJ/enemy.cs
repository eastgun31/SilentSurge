using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using ItemInfo;
//using TMPro;

public class Enemy : MonoBehaviour
{
    public enum EnemyState //wjr 상태머신
    {
        patrolling, hear, findtarget, die, sturn
    }
   // public TextMeshPro questionMark; // 물음표 UI 를 연결할 변수
   // public TextMeshPro exclamationMark; // 느낌표 UI를 연결할 변수
    public EnemyState state;

    NavMeshAgent m_enemy;
    [SerializeField] Vector3[] customDestinations; // 적 캐릭터가 이동할 목적지들의 좌표를 저장합니다.

    public GameObject bulletPrefab; // 총알의 프리팹
    public Transform bulletPos;
    public float bulletSpeed = 5f; // 총알의 발사 속도
    public GameObject gunmodal;
    public Vector3 targetpos;
    bool isShooting = false; // 총을 발사 중인지 여부를 나타내는 변수

    [SerializeField] private float stoppingDistance; // 적이 멈출 거리

    bool noactiving;

    //적 수정_김동건
    Sight sight;
    [SerializeField]
    private int enemyType;
    [SerializeField]
    private int indexcount;
    [SerializeField]
    private int naviindex;
    [SerializeField]
    private bool chasing;
    public bool hearSound;
    [SerializeField]
    private Transform[] bulletPoses;
    E_CoolTime cooltime;
    public AudioSource enemysound;
    Vector3 firstpos;

    Animator enemyAnim;
    string Walk = "Walk";
    string Shot = "Shot";
    string GunRuning = "GunRuning";
    string Death = "Death";
    string Death2 = "Death2";
    string Flash = "Flash";
    string PlayerListen = "PlayerListen";
    ////안쓰는변수
    //public bool m_triggered = false; // 트리거 충돌 여부를 나타냅니다.
    //public event Action<Player> PlayerSpotted; // 플레이어 발견 시 이벤트
    // 체력 관련 변수
    //[SerializeField] private int maxHealth = 100;
    //private int currentHealth;
    //int m_currentDestinationIndex = 0;
    //Transform m_player;
    //bool m_followingPlayer = false; // 적 캐릭터가 플레이어를 추적 중인지 여부를 나타냅니다.
    //bool isPlayerInSight = false;
    //int maxBulletCount = 10; // 최대 총알 개수
    //int currentBulletCount = 999; // 현재 총알 개수
    //int dLevel = 1;

    private void Awake()
    {
        //cooltime = new E_CoolTime();
        //sight = GetComponent<Sight>();
    }

    void Start()
    {
        //enemysound = GetComponent<AudioSource>();
        //stoppingDistance = 3f;
        //chasing = false;
        //stoppingDistance = 3f;
        //state = EnemyState.patrolling;
        //sight = GetComponent<Sight>();
        //noactiving = true;
        //hearSound = false;
        //m_enemy = GetComponent<NavMeshAgent>();
        //cooltime = new E_CoolTime();
        //StartCoroutine(EnemyStateCheck());
        //naviindex = 0;
        //enemyAnim = GetComponent<Animator>();
        //m_enemy.avoidancePriority = 50; // 벽을 피하기 위한 우선순위 설정

    }

    private void OnEnable()
    {
        StopAllCoroutines();
        cooltime = new E_CoolTime();
        sight = GetComponent<Sight>();
        sight.findT = false;
        m_enemy = GetComponent<NavMeshAgent>();
        m_enemy.ResetPath();
        m_enemy.velocity = Vector3.zero;
        StartCoroutine(EnemyStateCheck());
        enemysound = GetComponent<AudioSource>();
        m_enemy = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();
        stoppingDistance = 3f;
        noactiving = true;
        hearSound = false;
        isShooting = false;
        state = EnemyState.patrolling;
        EnemyPatrol();
        chasing = false;
        naviindex = 0;
        
        m_enemy.avoidancePriority = 50;
        if (indexcount == 98)
            customDestinations[0] = GameManager.instance.lv3PlayerPos;

    }

    void Update()
    {
        if (state != EnemyState.die && state == EnemyState.findtarget )
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
        else if(state == EnemyState.sturn)
        {
            EnenyAttackStop();
        }


        enemyAnim.SetFloat(Walk, m_enemy.velocity.magnitude);
        if (m_enemy.velocity.magnitude > 1f && enemysound.enabled)
        {
            if (!enemysound.isPlaying)
            {
                enemysound.Play();
            }
        }
        else if (m_enemy.velocity.magnitude <= 1f && enemysound.enabled)
        {
            if (enemysound.isPlaying)
            {
                enemysound.Stop();
            }
        }

    }

    public void ChaseSound(Vector3 position)
    {
        m_enemy.stoppingDistance = 0;
        m_enemy.SetDestination(position);
        if (noactiving)
            StartCoroutine(ChaseSoundRoutine(position)); // 대기 시간 5초 
    }

    IEnumerator ChaseSoundRoutine(Vector3 position)
    {
        noactiving = false;
        NeviClear();
        yield return cooltime.cool3sec;
        hearSound = false;
        noactiving = true;
        state = EnemyState.patrolling;
    }

    void NeviClear()    //적 네비 초기화
    {
        m_enemy.isStopped = true;
        m_enemy.velocity = Vector3.zero;
        m_enemy.stoppingDistance = stoppingDistance;
        m_enemy.isStopped = false;
    }
    void EnemyPatrol()  //적순찰
    {
        m_enemy.stoppingDistance = 1f;
        enemyAnim.SetBool(GunRuning, false);
        if (Vector3.Distance(transform.position, customDestinations[naviindex]) > 1f)
        {
            m_enemy.SetDestination(customDestinations[naviindex]);
        }
        else if (Vector3.Distance(transform.position, customDestinations[naviindex]) <= 1f || m_enemy.velocity.magnitude <= 0.1f)
        {
            naviindex++;

            if (naviindex == customDestinations.Length)
                naviindex = 0;
            m_enemy.SetDestination(customDestinations[naviindex]);
        }

    }

    void TargetChase()
    {
        m_enemy.stoppingDistance = 3f;
        transform.LookAt(sight.detectTarget.position);
        enemyAnim.SetBool(GunRuning, true); // 총을 들고 있을 때 설정
       m_enemy.SetDestination(sight.detectTarget.position);

        if (Vector3.Distance(transform.position, sight.detectTarget.position) <= 3f && !GameManager.instance.isDie &&state != EnemyState.die && state != EnemyState.sturn)
        {
            enemyAnim.SetBool(GunRuning, false);
            GameManager.instance.eonecollison = true;
            if (enemyType == 1 || enemyType == 2)
                StartCoroutine(Shoot()); // 총을 발사합니다.
            else if (enemyType == 3)
                StartCoroutine(CloseAttack());
            else if (enemyType == 4 && !isShooting)
                StartCoroutine(UdoShoot(sight.detectTarget.position));
        }
        if (Vector3.Distance(transform.position, sight.detectTarget.position) > 3f)
        {
            enemyAnim.SetBool(GunRuning, true);
            m_enemy.isStopped = false;
            m_enemy.SetDestination(sight.detectTarget.position);
        }
        else if(!sight.findT)
        {
            enemyAnim.SetBool(GunRuning, false);
            NeviClear();
            EnenyAttackStop();
            EnemyPatrol();
            state = EnemyState.patrolling;
        }
    }


    IEnumerator CloseAttack()
    {
        if (state == EnemyState.die || state == EnemyState.sturn || GameManager.instance.isDie)
            StopCoroutine(CloseAttack());

        m_enemy.stoppingDistance = 1;
        yield return cooltime.cool1sec;

        if (!isShooting)
        {
            if (state == EnemyState.die || GameManager.instance.isDie)
                yield break; 

            isShooting = true; // 발사 중 상태로 변경

            //transform.LookAt(pos);
            enemyAnim.SetTrigger(Shot);
            GameObject bulletObject = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
            // 총알 발사 후 일정 시간을 기다린 후 다음 동작으로 진행합니다.
            yield return cooltime.cool2sec; // 1초 뒤에 다시 총 발사
            isShooting=false;
        }
    }
    IEnumerator Shoot()
    {
        if (state == EnemyState.die || GameManager.instance.isDie)
            StopCoroutine(Shoot()); 

        m_enemy.stoppingDistance = stoppingDistance;

        yield return cooltime.cool1sec;
        if (!isShooting && !GameManager.instance.isDie)
        {
            if (state == EnemyState.die || state == EnemyState.sturn || GameManager.instance.isDie)
                yield break;

            isShooting = true; // 발사 중 상태로 변경

            m_enemy.isStopped = true;
            m_enemy.velocity = Vector3.zero;


            // 총알을 발사하는 동작을 수행합니다.
            //transform.LookAt(pos);

            GameObject bulletObject = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
            Rigidbody bulletRigid = bulletObject.GetComponent<Rigidbody>();
            // 총알을 생성하고 설정한 방향으로 발사합니다.
            enemyAnim.SetTrigger(Shot);
            if (enemyType == 2)
            {
                Shoot2();
                SoundManager.instance.EnemyEffect(1);
            }
            else if (enemyType == 1)
            {
                SoundManager.instance.EnemyEffect(0);
            }

            bulletRigid.velocity = bulletPos.forward * bulletSpeed;

            // 총알 발사 후 일정 시간을 기다린 후 다음 동작으로 진행
            yield return cooltime.cool2sec;
            isShooting = false;
        }
        
        m_enemy.isStopped = false;
    }
    void Shoot2()  //샷건 
    {
        for (int i = 0; i < bulletPoses.Length; i++)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, bulletPoses[i].transform.position, bulletPoses[i].rotation);
            Rigidbody bulletRigid = bulletObject.GetComponent<Rigidbody>();
            bulletRigid.velocity = bulletPoses[i].forward * bulletSpeed;
        }
    }
    IEnumerator UdoShoot(Vector3 pos) //바주카 (반유도 미사일)
    {
        if (state == EnemyState.die || GameManager.instance.isDie)
            StopCoroutine(UdoShoot(pos));

        m_enemy.stoppingDistance = stoppingDistance;

        yield return cooltime.cool1sec;
        if(!isShooting && !GameManager.instance.isDie)
        {
            if (state == EnemyState.die || state == EnemyState.sturn || GameManager.instance.isDie)
                yield break; ; 

            isShooting = true;
            m_enemy.isStopped = true;
            m_enemy.velocity = Vector3.zero;
            enemyAnim.SetTrigger(Shot);
            GameObject bulletObject = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
            Rigidbody bulletRigid = bulletObject.GetComponent<Rigidbody>();
            bulletObject.transform.LookAt(pos);
            bulletRigid.velocity = bulletPos.forward * bulletSpeed;
            yield return cooltime.cool2sec;
            isShooting = false;
        }
        m_enemy.isStopped = false;
    }

    //IEnumerator DelayTime(float type, WaitForSeconds delay)
    //{


    //    yield return delay; // 지정된 시간만큼 대기합니다.
    //    if (type == 1)
    //        isShooting = false; // 발사 종료 상태로 변경
    //    else if (type == 2)
    //        m_enemy.isStopped = false;
    //}

    IEnumerator EnemyStateCheck()
    {
        if (sight.findT && state != EnemyState.die && state != EnemyState.sturn)
        {
            //enemyAnim.SetBool(GunRuning, true);
            //enemyAnim.SetBool(Walk, false);
            //GameManager.instance.playerchasing = true;
            state = EnemyState.findtarget;

        }
        else if (sight.findT && hearSound && state != EnemyState.die && state != EnemyState.sturn)
        {
            //enemyAnim.SetBool(GunRuning, true);
            //enemyAnim.SetBool(Walk, false);
            state = EnemyState.findtarget;

        }
        else if (!sight.findT && hearSound && state != EnemyState.die && state != EnemyState.sturn)
        {

            // GameManager.instance.playerchasing = false;
            state = EnemyState.hear;
            if(sight.findT && state != EnemyState.die)
                state = EnemyState.findtarget;

        }
        else if (!sight.findT && !hearSound  && state != EnemyState.die && state != EnemyState.sturn)
        {

            //GameManager.instance.playerchasing = false;
            state = EnemyState.patrolling;
        }

        yield return cooltime.cool1sec;
        StartCoroutine(EnemyStateCheck());
    }
    //IEnumerator Levelstep()
    //{
    //    chasing = true;
    //    yield return cooltime.cool10sec;

    //    if (state == EnemyState.findtarget && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
    //    {

    //        //enemyAnim.SetBool(GunRuning, true);
    //        //enemyAnim.SetBool(Walk, false);
    //        EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level2;
    //        m_enemy.speed = 5f;
    //    }
    //    else if (state == EnemyState.findtarget && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level2)
    //    {

    //        //enemyAnim.SetBool(GunRuning, true);
    //        //enemyAnim.SetBool(Walk, false);
    //        EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;
    //        GameManager.instance.lv3PlayerPos = sight.detectTarget.position;
    //    }
    //    else if (state == EnemyState.findtarget && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level3)
    //    {

    //        //enemyAnim.SetBool(GunRuning, true);
    //        //enemyAnim.SetBool(Walk, false);
    //        EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;
    //        GameManager.instance.lv3PlayerPos = sight.detectTarget.position;
    //    }
    //    else if (state == EnemyState.patrolling && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
    //        m_enemy.speed = 3f;
    //    chasing = false;
    //}

    void EnenyAttackStop()
    {
        if(enemyType == 1 || enemyType ==2)
        {
            StopCoroutine(Shoot());
        }
        else if(enemyType == 3)
        {
            StopCoroutine(CloseAttack());
        }
        else if(enemyType == 4)
        {
            StopCoroutine(UdoShoot(sight.detectTarget.position));
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("AmSal"))
        {
            EnenyAttackStop();
            state = EnemyState.die;
            if(other.CompareTag("Bullet"))
            {
                Destroy(other.gameObject);
                enemyAnim.SetBool(Death, true);
            }
            else if(other.CompareTag("AmSal"))
            {
                enemyAnim.SetBool(Death2, true);
            }
                

            StartCoroutine(DeactivateWithDelay());
            if (indexcount != 99 || indexcount != 98)
            {
                GameManager.instance.existEnemy[indexcount] = false;
            }
            //StopAllCoroutines();
           
        }
        else if (other.CompareTag(Flash))
        {
            EnenyAttackStop();
            m_enemy.isStopped = true;
            m_enemy.velocity = Vector3.zero;
            state = EnemyState.sturn;
            enemyAnim.SetTrigger(Flash);
            StartCoroutine(ReactivateMovementAfterDelay(5f));
        }

        if(other.CompareTag(PlayerListen))
        {
            enemysound.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PlayerListen))
        {
            enemysound.enabled = false;
        }
    }



    private IEnumerator DeactivateWithDelay()
    {
        m_enemy.ResetPath();
        m_enemy.isStopped = true;
        m_enemy.velocity = Vector3.zero;
        
        yield return new WaitForSeconds(2f);
        EnenyAttackStop();
        gameObject.SetActive(false);
    }


    private IEnumerator ReactivateMovementAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);
        m_enemy.isStopped = false;
        if(sight.findT)
            state = EnemyState.findtarget;
        else
            state = EnemyState.patrolling;
        //yield return new WaitForSeconds(1f);
    
    }

    //IEnumerator ShootRoutine()
    //{
    //    int shotsFired = 0;
    //    while (shotsFired < 10)
    //    {
    //        // 총알 발사
    //        shoot(sight.detectTarget.position);

    //        // 발사 후 대기 시간
    //        yield return new WaitForSeconds(1f);

    //        shotsFired++;
    //    }
    //}
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



    //void SetNextDestination()
    //{
    //    if (!sight.findT && !m_followingPlayer && customDestinations.Length > 0)
    //    {
    //        m_enemy.isStopped = false;

    //        // 기본적인 목적지 설정
    //        m_enemy.SetDestination(customDestinations[m_currentDestinationIndex]);
    //        m_currentDestinationIndex = (m_currentDestinationIndex + 1) % customDestinations.Length;
    //    }
    //}

    //bool CanSeePlayer() // 적 캐릭터가 플레이어를 시야에 볼 수 있는지 여부를 판단하고 반환합니다.
    //{
    //    if (m_player == null)
    //        return false;

    //    Vector3 directionToPlayer = m_player.position - transform.position; // 플레이어와 적 사이의 방향을 계산합니다.
    //    if (Vector3.Angle(transform.forward, directionToPlayer) < 90f && directionToPlayer.magnitude < stoppingDistance)
    //    {
    //        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, Mathf.Infinity, ~LayerMask.GetMask("Wall"))) // 벽을 제외한 레이어에서만 충돌을 검사합니다.
    //        {
    //            if (hit.collider.CompareTag("Player"))
    //            {
    //                return true;
    //            }
    //        }
    //    }

    //    return false;
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Sound"))
    //    {
    //        m_triggered = true; // 트리거 충돌 시 상태를 변경합니다.
    //    }
    //}


    // 업데이트 원본 
    //void Update()
    //    {
    //    if (state == EnemyState.findtarget)
    //        {
    //        enemyAnim.SetBool(Walk, false);
    //        enemyAnim.SetBool(GunRuning, true);
    //        TargetChase();
    //        }
    //    else if (state == EnemyState.hear)
    //        {

    //        ChaseSound(targetpos);
    //        }
    //    else if (state == EnemyState.patrolling)
    //        {

    //        EnemyPatrol();
    //        }

    //    if (indexcount == 98)
    //        customDestinations[0] = GameManager.instance.lv3PlayerPos;
    //    }



}



