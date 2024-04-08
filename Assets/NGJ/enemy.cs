using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;

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

    

    public bool m_triggered = false; // Ʈ���� �浹 ���θ� ��Ÿ���ϴ�.
    public Vector3 targetpos;

    int dLevel = 1;

    [SerializeField] private float stoppingDistance = 1f; // ���� ���� �Ÿ�

    public event Action<Player> PlayerSpotted; // �÷��̾� �߰� �� �̺�Ʈ
    bool noactiving;

    // ü�� ���� ����
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    //�� ����_�赿��
    Sight sight;
    [SerializeField]
    private int indexcount;

    void Start()
    {
        state = EnemyState.patrolling;
        sight = GetComponent<Sight>(); 
        noactiving = true;
        m_enemy = GetComponent<NavMeshAgent>();
        m_enemy.stoppingDistance = 0; // ���� ���� �Ÿ� ����
        m_enemy.avoidancePriority = 50; // ���� ���ϱ� ���� �켱���� ����
        //m_player = GameObject.FindGameObjectWithTag("Player").transform;
        //SetNextDestination();

        // �ʱ� ü�� ����
        currentHealth = maxHealth;
        indexcount = 0;
    }

    void Update()
    {
        if (sight.findT)
        {
            state = EnemyState.findtarget;
            Debug.Log("�÷��̾�߰�");
            if(!m_followingPlayer)
                NeviClear();
            m_enemy.SetDestination(sight.detectTarget.position); // �÷��̾ �߰��ϸ� �÷��̾ ���󰩴ϴ�.

            //// �÷��̾ �߰����� �� �̺�Ʈ �߻�
            //PlayerSpotted?.Invoke(m_player.GetComponent<Player>());
        }
        else if(state == EnemyState.findtarget && !sight.findT)
            state = EnemyState.patrolling;
        else if (!sight.findT && state == EnemyState.hear)
        {
            m_enemy.stoppingDistance = 0f;
            ChasePlayer(targetpos); // Ʈ���� �浹 �� �÷��̾ ���󰩴ϴ�.
        }
        else if(!m_triggered && !sight.findT)
        {
            m_followingPlayer = false;
            m_enemy.isStopped = false;
            //if (!m_enemy.pathPending && m_enemy.remainingDistance < stoppingDistance)
            //{
            //    SetNextDestination(); // ���ÿ��� �⺻���� �������� �̵��մϴ�.
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
            StartCoroutine(ChasePlayerRoutine(position, 5f)); // ��� �ð� 5�� 
    }

    IEnumerator ChasePlayerRoutine(Vector3 position, float chaseDuration)
    {
        noactiving = false;
        yield return new WaitForSeconds(chaseDuration);
        m_triggered = false; // Ʈ���� �浹 ���� ����
        noactiving = true;
        m_enemy.stoppingDistance = stoppingDistance;
        //SetNextDestination();
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
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // �Ѿ� ����� �浹�̸鼭 �÷��̾� �Ѿ��� ��쿡�� ���ظ� �޽��ϴ�.
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
            TakeDamage(10); // 10�� �������� �Խ��ϴ�.
        }
    }

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
