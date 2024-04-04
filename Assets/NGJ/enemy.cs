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

    public event Action<Player> PlayerSpotted; // �÷��̾� �߰� �� �̺�Ʈ

    // ü�� ���� ����
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        m_enemy = GetComponent<NavMeshAgent>();
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        SetNextDestination();

        // �ʱ� ü�� ����
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

                // �÷��̾ �߰����� �� �̺�Ʈ �߻�
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
                // ���� 2 �Ǵ� ���� 3�� ��� �߰� ���� ����
                // ���� ���, Ư�� ��ǥ�� �̵��ϵ��� ������ �� �ֽ��ϴ�.
            }
            else
            {
                // �⺻���� ������ ����
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
            // ��ֹ��� �����ϸ� �������� �ٽ� ����
            SetNextDestination();
        }

        // �Ѿ� ����� �浹�̸鼭 �÷��̾� �Ѿ��� ��쿡�� ���ظ� ����
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10); // 10�� �������� ����
        }
    }

    // ���ظ� �Դ� �޼���
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die(); // ü���� 0 ���ϰ� �Ǹ� ��� ó��
        }
    }

    // ��� ó�� �޼���
    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject); // �� ������Ʈ �ı�
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

//    // �� ĳ������ �þ߰��� �þ� �Ÿ�
//    public float viewAngle = 1190f;
//    public float viewDistance = 1110f;

//    // ü�� ���� ����
//    [SerializeField] private int maxHealth = 100;
//    private int currentHealth;

//    void Start()
//    {
//        m_enemy = GetComponent<NavMeshAgent>();
//        m_player = GameObject.FindGameObjectWithTag("Player").transform;
//        SetNextDestination();

//        // �ʱ� ü�� ����
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
//            // ��ֹ��� �����ϸ� �������� �ٽ� ����
//            SetNextDestination();
//        }

//        // �÷��̾ ���� ��ü�� �浹�� ���
//        if (collision.gameObject.CompareTag("ThrownObject"))
//        {
//            // ���ۺ��� ���� ����
//            StartCoroutine(Spinning());
//        }
//    }

//    // ���ظ� �Դ� �޼���
//    void TakeDamage(int damage)
//    {
//        currentHealth -= damage;
//        if (currentHealth <= 0)
//        {
//            Die(); // ü���� 0 ���ϰ� �Ǹ� ��� ó��
//        }
//    }

//    // ��� ó�� �޼���
//    void Die()
//    {
//        Debug.Log("Enemy died!");
//        Destroy(gameObject); // �� ������Ʈ �ı�
//    }

//    // ���ۺ��� ���� �ڷ�ƾ
//    IEnumerator Spinning()
//    {
//        float duration = 2f; // ���ۺ��� ���ư��� �ð�
//        float elapsedTime = 0f;

//        while (elapsedTime < duration)
//        {
//            transform.Rotate(Vector3.up, 360f * Time.deltaTime / duration); // 1�ʿ� 360�� ȸ��
//            elapsedTime += Time.deltaTime;
//            yield return null;
//        }
//    }
//}


//���� �ڵ��� �÷��̾ ���� ��ü�� ������ ���ڸ����� ���ۺ��� ���� �ڵ带 �߰��� (����ź��)


//using UnityEngine;
//using UnityEngine.AI;

//public class Enemy : MonoBehaviour
//{
//    NavMeshAgent m_enemy;
//    [SerializeField] Vector3[] customDestinations;
//    int m_currentDestinationIndex = 0;
//    Transform m_player;
//    bool m_followingPlayer = false;

//    // �� ĳ������ �þ߰��� �þ� �Ÿ�
//    public float viewAngle = 1190f;
//    public float viewDistance = 1110f;

//    // ü�� ���� ����
//    [SerializeField] private int maxHealth = 100;
//    private int currentHealth;

//    // ���� ���� ����
//    public AudioClip soundClip; // �̵��� �� ����� ���� Ŭ��
//    private AudioSource audioSource;

//    void Start()
//    {
//        m_enemy = GetComponent<NavMeshAgent>();
//        m_player = GameObject.FindGameObjectWithTag("Player").transform;
//        SetNextDestination();

//        // �ʱ� ü�� ����
//        currentHealth = maxHealth;

//        // AudioSource ������Ʈ ��������
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
//            // ��ֹ��� �����ϸ� �������� �ٽ� ����
//            SetNextDestination();
//        }

//        // �Ѿ� ����� �浹�̸鼭 �÷��̾� �Ѿ��� ��쿡�� ���ظ� ����
//        if (collision.gameObject.CompareTag("Bullet"))
//        {
//            TakeDamage(10); // 10�� �������� ����
//        }
//    }

//    // ���ظ� �Դ� �޼���
//    void TakeDamage(int damage)
//    {
//        currentHealth -= damage;
//        if (currentHealth <= 0)
//        {
//            Die(); // ü���� 0 ���ϰ� �Ǹ� ��� ó��
//        }
//        else
//        {
//            // ���ظ� ���� �� ���� ���
//            if (soundClip != null && audioSource != null)
//            {
//                audioSource.clip = soundClip;
//                audioSource.Play();
//            }
//        }
//    }

//    // ��� ó�� �޼���
//    void Die()
//    {
//        Debug.Log("Enemy died!");
//        Destroy(gameObject); // �� ������Ʈ �ı�
//    }
//}


//���� �ڵ�� �Ҹ��� ������ �����̴� �ڵ带 �߰��� 
