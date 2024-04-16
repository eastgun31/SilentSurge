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
  
    public GameObject bulletPrefab; // �Ѿ��� ������
    public Transform bulletPos;
    public float bulletSpeed = 5f; // �Ѿ��� �߻� �ӵ�
    public GameObject gunmodal;
    public Vector3 targetpos;
    bool isShooting = false; // ���� �߻� ������ ���θ� ��Ÿ���� ����

    [SerializeField] private float stoppingDistance; // ���� ���� �Ÿ�

    bool noactiving;

    //�� ����_�赿��
    Sight sight;
    [SerializeField]
    private int enemyType;
    [SerializeField]
    private int indexcount;
    [SerializeField]
    private bool chasing;
    public bool hearSound;
    E_CoolTime cooltime;

    ////�Ⱦ��º���
    //public bool m_triggered = false; // Ʈ���� �浹 ���θ� ��Ÿ���ϴ�.
    //public event Action<Player> PlayerSpotted; // �÷��̾� �߰� �� �̺�Ʈ
    // ü�� ���� ����
    //[SerializeField] private int maxHealth = 100;
    //private int currentHealth;
    //int m_currentDestinationIndex = 0;
    //Transform m_player;
    //bool m_followingPlayer = false; // �� ĳ���Ͱ� �÷��̾ ���� ������ ���θ� ��Ÿ���ϴ�.
    //bool isPlayerInSight = false;
    //int maxBulletCount = 10; // �ִ� �Ѿ� ����
    //int currentBulletCount = 999; // ���� �Ѿ� ����
    //int dLevel = 1;

    void Start()
    {

        chasing = false;
        stoppingDistance = 3f;
        state = EnemyState.patrolling;
        sight = GetComponent<Sight>();
        noactiving = true;
        hearSound = false;
        m_enemy = GetComponent<NavMeshAgent>();
        cooltime = new E_CoolTime();
        StartCoroutine(EnemyStateCheck());
        indexcount = 0;

        m_enemy.avoidancePriority = 50; // ���� ���ϱ� ���� �켱���� ����

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

        yield return cooltime.cool3sec;
        hearSound = false;
        noactiving = true;
        NeviClear();
        state = EnemyState.patrolling;
    }

    void NeviClear()    //�� �׺� �ʱ�ȭ
    {
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

        if (Vector3.Distance(transform.position, sight.detectTarget.position) <= 3f)
        {
            if(enemyType == 1 ||  enemyType == 2)
                shoot(sight.detectTarget.position); // ���� �߻��մϴ�.
            else if(enemyType == 3)
            {

            }
            else if(enemyType == 4)
            {

            }
        }
        else if(Vector3.Distance(transform.position, sight.detectTarget.position) > 3f)
        {
            m_enemy.isStopped = false;
        }
    }
   
    void shoot(Vector3 pos)
    {
        if (!isShooting )
        {
            isShooting = true; // �߻� �� ���·� ����

            m_enemy.isStopped = true;
            // �Ѿ��� �߻��ϴ� ������ �����մϴ�.
            transform.LookAt(pos);
            GameObject bulletObject = Instantiate(bulletPrefab, gunmodal.transform.position, bulletPos.rotation);
            Rigidbody bulletRigid = bulletObject.GetComponent<Rigidbody>();
            // �Ѿ��� �����ϰ� ������ �������� �߻��մϴ�.
            if (enemyType == 2)
                shoot2();
            bulletRigid.velocity = bulletPos.forward * bulletSpeed;

            // �Ѿ� �߻� �� ���� �ð��� ��ٸ� �� ���� �������� �����մϴ�.
            StartCoroutine(DelayTime(1f,cooltime.cool5sec)); // 1�� �ڿ� �ٽ� �� �߻�
        }
    }
    void shoot2()
    {
        GameObject bulletObject1 = Instantiate(bulletPrefab, gunmodal.transform.position, bulletPos.rotation);
        GameObject bulletObject2 = Instantiate(bulletPrefab, gunmodal.transform.position, bulletPos.rotation);
        Rigidbody bulletRigid1 = bulletObject1.GetComponent<Rigidbody>();
        Rigidbody bulletRigid2 = bulletObject2.GetComponent<Rigidbody>();
        //bulletObject1.transform.rotation = Quaternion.Euler(new Vector3(0,60,0));
        //bulletObject2.transform.rotation = Quaternion.Euler(new Vector3(0, 120, 0));
        bulletRigid1.MoveRotation(Quaternion.Euler(0, 60, 0));
        bulletRigid2.MoveRotation(Quaternion.Euler(0, 120, 0));
        bulletRigid1.velocity = new Vector3(0, 60, 0) * bulletSpeed;
        bulletRigid2.velocity = new Vector3(0, 120, 0) * bulletSpeed;
    }

    IEnumerator DelayTime(float type, WaitForSeconds delay)
    {
        yield return delay; // ������ �ð���ŭ ����մϴ�.
        if (type == 1)
            isShooting = false; // �߻� ���� ���·� ����
        else if (type == 2)
            m_enemy.isStopped = false;
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
            yield return cooltime.cool1sec;
            GameManager.instance.playerchasing = false;
            state = EnemyState.patrolling;
        }

        yield return cooltime.cool1sec;
        StartCoroutine(EnemyStateCheck());
    }
    IEnumerator Levelstep()
    {
        chasing = true;
        yield return cooltime.cool10sec;

        if (state == EnemyState.findtarget && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
        {
            EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level2;
        }
        else if (state == EnemyState.findtarget && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level2)
        {
            EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;
        }
        else if (state == EnemyState.findtarget && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level3)
        {
            EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;
        }
        chasing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject, 1f);
            if (indexcount != 99)
                GameManager.instance.existEnemy[indexcount] = false;
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Flash"))
        {
            m_enemy.isStopped = true;
            m_enemy.velocity = Vector3.zero;
            StartCoroutine(DelayTime(2f, cooltime.cool5sec));
            //m_enemy.isStopped = false;
        }
    }

    //IEnumerator ShootRoutine()
    //{
    //    int shotsFired = 0;
    //    while (shotsFired < 10)
    //    {
    //        // �Ѿ� �߻�
    //        shoot(sight.detectTarget.position);

    //        // �߻� �� ��� �ð�
    //        yield return new WaitForSeconds(1f);

    //        shotsFired++;
    //    }
    //}
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



    //void SetNextDestination()
    //{
    //    if (!sight.findT && !m_followingPlayer && customDestinations.Length > 0)
    //    {
    //        m_enemy.isStopped = false;

    //        // �⺻���� ������ ����
    //        m_enemy.SetDestination(customDestinations[m_currentDestinationIndex]);
    //        m_currentDestinationIndex = (m_currentDestinationIndex + 1) % customDestinations.Length;
    //    }
    //}

    //bool CanSeePlayer() // �� ĳ���Ͱ� �÷��̾ �þ߿� �� �� �ִ��� ���θ� �Ǵ��ϰ� ��ȯ�մϴ�.
    //{
    //    if (m_player == null)
    //        return false;

    //    Vector3 directionToPlayer = m_player.position - transform.position; // �÷��̾�� �� ������ ������ ����մϴ�.
    //    if (Vector3.Angle(transform.forward, directionToPlayer) < 90f && directionToPlayer.magnitude < stoppingDistance)
    //    {
    //        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, Mathf.Infinity, ~LayerMask.GetMask("Wall"))) // ���� ������ ���̾���� �浹�� �˻��մϴ�.
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
    //        m_triggered = true; // Ʈ���� �浹 �� ���¸� �����մϴ�.
    //    }
    //}

}