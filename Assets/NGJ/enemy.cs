using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using ItemInfo;

public class Enemy : MonoBehaviour
{
    public enum EnemyState //wjr ���¸ӽ�
    {
        patrolling, hear, findtarget
    }

    public EnemyState state;

    NavMeshAgent m_enemy;
    [SerializeField] Vector3[] customDestinations; // �� ĳ���Ͱ� �̵��� ���������� ��ǥ�� �����մϴ�.
    int m_currentDestinationIndex = 0;
    
    Transform m_player;
    bool m_followingPlayer = false; // �� ĳ���Ͱ� �÷��̾ ���� ������ ���θ� ��Ÿ���ϴ�.
    bool isPlayerInSight = false;  
    public GameObject bulletPrefab; // �Ѿ��� ������
    public Transform bulletPos;
    public float bulletSpeed = 20f; // �Ѿ��� �߻� �ӵ�
    public GameObject gunmodal;
    public bool m_triggered = false; // Ʈ���� �浹 ���θ� ��Ÿ���ϴ�.
    public Vector3 targetpos;

    int maxBulletCount = 10; // �ִ� �Ѿ� ����
    int currentBulletCount = 999; // ���� �Ѿ� ����

    bool isShooting = false; // ���� �߻� ������ ���θ� ��Ÿ���� ����
  
    int dLevel = 1;

    [SerializeField] private float stoppingDistance; // ���� ���� �Ÿ�

    public event Action<Player> PlayerSpotted; // �÷��̾� �߰� �� �̺�Ʈ
    bool noactiving;

    // ü�� ���� ����
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    //�� ����_�赿��
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

        m_enemy.avoidancePriority = 50; // ���� ���ϱ� ���� �켱���� ����
                                        //m_player = GameObject.FindGameObjectWithTag("Player").transform;
                                        //SetNextDestination();

        // �ʱ� ü�� ����
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
        //Debug.Log("�Ҹ�����");
        m_enemy.SetDestination(position);
        if (noactiving)
            StartCoroutine(ChaseSoundRoutine(position)); // ��� �ð� 5�� 
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

    void NeviClear()    //�� �׺� �ʱ�ȭ
    {
        m_followingPlayer = true;
        m_enemy.isStopped = true;
        m_enemy.velocity = Vector3.zero;
        m_enemy.stoppingDistance = stoppingDistance;
        m_enemy.isStopped = false;
        indexcount = 0;
    }
    void EnemyPatrol()  //������
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
            shoot(sight.detectTarget.position); // ���� �߻��մϴ�.
        }

    }

    IEnumerator ShootRoutine()
    {
        int shotsFired = 0;
        while (shotsFired < 10)
        {
            // �Ѿ� �߻�
            shoot(sight.detectTarget.position);

            // �߻� �� ��� �ð�
            yield return new WaitForSeconds(1f);

            shotsFired++;
        }
    }

   
    void shoot(Vector3 pos)
    {
        if (!isShooting )
        {
            isShooting = true; // �߻� �� ���·� ����
            //currentBulletCount--; // �Ѿ� ���� ����

            m_enemy.isStopped = true;
            // �Ѿ��� �߻��ϴ� ������ �����մϴ�.
            Debug.Log("Enemy: Shooting! Remaining Bullets: " );

            GameObject bulletObject = Instantiate(bulletPrefab, gunmodal.transform.position, bulletPos.rotation);
            Rigidbody bulletRigid = bulletObject.GetComponent<Rigidbody>();
            // �Ѿ��� ������ �� ĳ���Ͱ� �ٶ󺸴� �������� �����մϴ�.
            Vector3 shootDirection = transform.forward;

            // �Ѿ��� �����ϰ� ������ �������� �߻��մϴ�.
            bulletRigid.velocity = bulletPos.forward * bulletSpeed;

            // �Ѿ� �߻� �� ���� �ð��� ��ٸ� �� ���� �������� �����մϴ�.
            
            StartCoroutine(ShootDelay(0.5f)); // 1�� �ڿ� �ٽ� �� �߻�
        }
        //else if (currentBulletCount <= 0)
        //{
        //    Debug.Log("Out of bullets! Reloading...");
        //    StartCoroutine(ReloadDelay(3f)); // �Ѿ��� ��� ����� ��� 3�� �Ŀ� ������
        //}

    }
    //void ResumePatrollingAfterDelay()
    //{
    //    StartCoroutine(ResumePatrollingCoroutine(20f)); // 20�� �ڿ� �ٽ� ���� �簳
    //}

    //IEnumerator ResumePatrollingCoroutine(float delay)
    //{
    //    yield return new WaitForSeconds(20f); // ������ �ð���ŭ ����մϴ�.
    //    m_enemy.isStopped = false; // ���� �簳
    //}
    //IEnumerator ReloadDelay(float delay)
    //{
    //    yield return new WaitForSeconds(5f); // ������ ���
    //    currentBulletCount = maxBulletCount; // �ִ� �Ѿ� ������ ������
    //}

    IEnumerator ShootDelay(float delay)
    {
        yield return new WaitForSeconds(5f); // ������ �ð���ŭ ����մϴ�.
        isShooting = false; // �߻� ���� ���·� ����
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

            // �⺻���� ������ ����
            m_enemy.SetDestination(customDestinations[m_currentDestinationIndex]);
            m_currentDestinationIndex = (m_currentDestinationIndex + 1) % customDestinations.Length;
        }
    }

    bool CanSeePlayer() // �� ĳ���Ͱ� �÷��̾ �þ߿� �� �� �ִ��� ���θ� �Ǵ��ϰ� ��ȯ�մϴ�.
    {
        if (m_player == null)
            return false;

        Vector3 directionToPlayer = m_player.position - transform.position; // �÷��̾�� �� ������ ������ ����մϴ�.
        if (Vector3.Angle(transform.forward, directionToPlayer) < 90f && directionToPlayer.magnitude < stoppingDistance)
        {
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, Mathf.Infinity, ~LayerMask.GetMask("Wall"))) // ���� ������ ���̾���� �浹�� �˻��մϴ�.
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
    //        m_triggered = true; // Ʈ���� �浹 �� ���¸� �����մϴ�.
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
    //    // �浹�� ��ü�� ���̰ų� �÷��̾��� ���
    //    if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
    //    {
    //        // �浹�� �Ѿ��� �� �� �Ŀ� �����մϴ�.
    //        StartCoroutine(DestroyBulletAfterDelay(0.1f)); // 1�� �Ŀ� �����ϵ��� ����
    //    }
    //}

    

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            m_enemy.isStopped = true;
            m_enemy.velocity = Vector3.zero;
            Die(); // ü���� 0 ���ϰ� �Ǹ� ��� ó���մϴ�.
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject); // �� ������Ʈ�� �ı��մϴ�.
    }
}