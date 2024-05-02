using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using ItemInfo;
//using TMPro;

public class Enemy : MonoBehaviour
{
    public enum EnemyState //wjr ���¸ӽ�
    {
        patrolling, hear, findtarget, die, sturn
    }
   // public TextMeshPro questionMark; // ����ǥ UI �� ������ ����
   // public TextMeshPro exclamationMark; // ����ǥ UI�� ������ ����
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
    private int naviindex;
    [SerializeField]
    private bool chasing;
    public bool hearSound;
    [SerializeField]
    private Transform[] bulletPoses;
    E_CoolTime cooltime;
    public AudioSource enemysound;


    Animator enemyAnim;
    string Walk = "Walk";
    string Shot = "Shot";
    string GunRuning = "GunRuning";
    string Death = "Death";
    string Flash = "Flash";
    string PlayerListen = "PlayerListen";
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
        enemysound = GetComponent<AudioSource>();  
     
         chasing = false;
        stoppingDistance = 3f;
        state = EnemyState.patrolling;
        sight = GetComponent<Sight>();
        noactiving = true;
        hearSound = false;
        m_enemy = GetComponent<NavMeshAgent>();
        cooltime = new E_CoolTime();
        StartCoroutine(EnemyStateCheck());
        naviindex = 0;
        enemyAnim = GetComponent<Animator>();
        m_enemy.avoidancePriority = 50; // ���� ���ϱ� ���� �켱���� ����

        }

    private void OnEnable()
    {
        state = EnemyState.patrolling;

        if (indexcount == 98)
            customDestinations[0] = GameManager.instance.lv3PlayerPos;

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
            m_enemy.isStopped = false;
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
                Debug.Log("�߼Ҹ����");
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
        m_enemy.SetDestination(position);
        if (noactiving)
            StartCoroutine(ChaseSoundRoutine(position)); // ��� �ð� 5�� 
    }

    IEnumerator ChaseSoundRoutine(Vector3 position)
    {
        noactiving = false;
        m_enemy.stoppingDistance = 0;


        yield return cooltime.cool2sec;
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
    }
    void EnemyPatrol()  //������
    {
        enemyAnim.SetBool(GunRuning, false);
        if (Vector3.Distance(transform.position, customDestinations[naviindex]) > 1f)
        {
            m_enemy.SetDestination(customDestinations[naviindex]);
        }
        else if (Vector3.Distance(transform.position, customDestinations[naviindex]) <= 1f)
        {
            naviindex++;

            if (naviindex == customDestinations.Length)
                naviindex = 0;
            m_enemy.SetDestination(customDestinations[naviindex]);
        }
    }

    void TargetChase()
    {
        transform.LookAt(sight.detectTarget.position);
        enemyAnim.SetBool(GunRuning, true); // ���� ��� ���� �� ����
       m_enemy.stoppingDistance = stoppingDistance;
       m_enemy.SetDestination(sight.detectTarget.position);

        Debug.Log("�߰ݽ���");
        if (Vector3.Distance(transform.position, sight.detectTarget.position) <= 3f && !GameManager.instance.isDie &&state != EnemyState.die && state != EnemyState.sturn)
        {
            Debug.Log("��ݽ���");
            enemyAnim.SetBool(GunRuning, false);

            if (enemyType == 1 || enemyType == 2)
                StartCoroutine(Shoot()); // ���� �߻��մϴ�.
            else if (enemyType == 3)
                StartCoroutine(CloseAttack());
            else if (enemyType == 4 && !isShooting)
                StartCoroutine(UdoShoot());
        }
        if (Vector3.Distance(transform.position, sight.detectTarget.position) > 3f)
        {
            Debug.Log("�ٽ��߰ݽ���");
            enemyAnim.SetBool(GunRuning, true);
            m_enemy.isStopped = false;
        }
        else if(!sight.findT)
        {
            Debug.Log("����������");
            state = EnemyState.patrolling;
            EnemyPatrol();
        }
    }


    IEnumerator CloseAttack()
    {
        m_enemy.stoppingDistance = 1;
        yield return cooltime.cool1sec;

        if (!isShooting)
        {
            isShooting = true; // �߻� �� ���·� ����

            //transform.LookAt(pos);
            enemyAnim.SetTrigger(Shot);
            GameObject bulletObject = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
            Destroy(bulletObject,1f);
            // �Ѿ� �߻� �� ���� �ð��� ��ٸ� �� ���� �������� �����մϴ�.
            yield return cooltime.cool2sec; // 1�� �ڿ� �ٽ� �� �߻�
            isShooting=false;
        }
    }
    IEnumerator Shoot()
    {
        yield return cooltime.cool1sec;
        if (!isShooting)
        {
            isShooting = true; // �߻� �� ���·� ����

            m_enemy.isStopped = true;
            m_enemy.velocity = Vector3.zero;
            

            // �Ѿ��� �߻��ϴ� ������ �����մϴ�.
            //transform.LookAt(pos);

            GameObject bulletObject = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
            Rigidbody bulletRigid = bulletObject.GetComponent<Rigidbody>();
            // �Ѿ��� �����ϰ� ������ �������� �߻��մϴ�.
            enemyAnim.SetTrigger(Shot);
            if (enemyType == 2)
            {
                Shoot2();
                SoundManager.instance.EnemyEffect(1);
            }
            else if(enemyType == 1) 
            {
                SoundManager.instance.EnemyEffect(0);
            }
            
            bulletRigid.velocity = bulletPos.forward * bulletSpeed;

            // �Ѿ� �߻� �� ���� �ð��� ��ٸ� �� ���� �������� ����
            yield return cooltime.cool2sec; 
            isShooting = false;
        }
        m_enemy.isStopped = false;
    }
    void Shoot2()  //���� 
    {
        for (int i = 0; i < bulletPoses.Length; i++)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, bulletPoses[i].transform.position, bulletPoses[i].rotation);
            Rigidbody bulletRigid = bulletObject.GetComponent<Rigidbody>();
            bulletRigid.velocity = bulletPoses[i].forward * bulletSpeed;
        }
    }
    IEnumerator UdoShoot() //����ī (������ �̻���)
    {
        yield return cooltime.cool1sec;
        if(!isShooting)
        {
            isShooting = true;
            m_enemy.isStopped = true;
            m_enemy.velocity = Vector3.zero;
            //transform.LookAt(pos);
            enemyAnim.SetTrigger(Shot);
            GameObject bulletObject = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
            Rigidbody bulletRigid = bulletObject.GetComponent<Rigidbody>();
            yield return cooltime.cool2sec;
            //bulletObject.transform.LookAt(pos);
            bulletRigid.velocity = bulletPos.forward * bulletSpeed;
            bulletRigid.velocity = bulletPos.forward * bulletSpeed;
            yield return cooltime.cool2sec;
            isShooting = false;
        }
        m_enemy.isStopped = false;
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
            //enemyAnim.SetBool(GunRuning, true);
            //enemyAnim.SetBool(Walk, false);
            //GameManager.instance.playerchasing = true;
            state = EnemyState.findtarget;

        }
        else if (sight.findT && hearSound)
        {
            //enemyAnim.SetBool(GunRuning, true);
            //enemyAnim.SetBool(Walk, false);
            state = EnemyState.findtarget;

        }
        else if (!sight.findT && hearSound)
        {

            // GameManager.instance.playerchasing = false;
            state = EnemyState.hear;
            if(sight.findT)
                state = EnemyState.findtarget;

        }
        else if (!sight.findT && !hearSound)
        {

            yield return cooltime.cool1sec;
            //GameManager.instance.playerchasing = false;
            state = EnemyState.patrolling;
            m_enemy.isStopped = false ;
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
            StopCoroutine(UdoShoot());
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            if (indexcount != 99 || indexcount != 98)
            {
                GameManager.instance.existEnemy[indexcount] = false;
                //if (GameManager.instance.scenenum == 1)
                //    GameManager.instance.existEnemy[indexcount] = false;
                //else if (GameManager.instance.scenenum == 2)
                //    GameManager.instance.existEnemy2[indexcount] = false;
            }
            //StopAllCoroutines();
            state = EnemyState.die;
            EnenyAttackStop();
            m_enemy.isStopped = true;
            m_enemy.velocity = Vector3.zero;
            enemyAnim.SetBool(Death,true);

            StartCoroutine(DeactivateWithDelay());
        }
        else if (other.CompareTag(Flash))
        {
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

        yield return new WaitForSeconds(2f);


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


    // ������Ʈ ���� 
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



