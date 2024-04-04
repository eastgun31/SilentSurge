using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    NavMeshAgent m_enemy;
    [SerializeField] Vector3[] customDestinations; // �� �迭�� �� ĳ���Ͱ� �̵��� ���������� ��ǥ�� �����մϴ�.
    int m_currentDestinationIndex = 0;
    Transform m_player;
    bool m_followingPlayer = false; // �� ������ �� ĳ���Ͱ� �÷��̾ ���� ������ ���θ� ��Ÿ���ϴ�.
    int dLevel = 1;

    [SerializeField] private float stoppingDistance = 2f; // ���� ���� �Ÿ�

    public event Action<Player> PlayerSpotted; // �÷��̾� �߰� �� �̺�Ʈ

    // ü�� ���� ����
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [SerializeField] private float listenRadius = 10f; // ��� ����

    AudioSource audioSource; // ���带 ��� ������ �� ����� �ҽ�

    void Start()
    {
        m_enemy = GetComponent<NavMeshAgent>();
        m_enemy.stoppingDistance = stoppingDistance; // ���� ���� �Ÿ� ����
        m_enemy.avoidancePriority = 50; // ���� ���ϱ� ���� �켱���� ����
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        SetNextDestination();

        // �ʱ� ü�� ����
        currentHealth = maxHealth;

      
    }

    void Update()
    {
        if (CanHearPlayerSound())
        {
            if (!m_followingPlayer)
            {
                m_followingPlayer = true;
                m_enemy.SetDestination(m_player.position);

                // �÷��̾ �߰����� �� �̺�Ʈ �߻�
                PlayerSpotted?.Invoke(m_player.GetComponent<Player>());
            }
        }
        else
        {
            m_followingPlayer = false;

            if (!m_enemy.pathPending && m_enemy.remainingDistance < stoppingDistance)
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

            if (dLevel == 1)
            {
                // �⺻���� ������ ����
                m_enemy.SetDestination(customDestinations[m_currentDestinationIndex]);
                m_currentDestinationIndex = (m_currentDestinationIndex + 1) % customDestinations.Length;
            }
        }
    }

    bool CanHearPlayerSound() // �� ĳ���Ͱ� �÷��̾��� �Ҹ��� ���� �� �ִ��� ���θ� �Ǵ��ϰ� ��ȯ.
    {
        if (m_player == null)
            return false;

        float distanceToPlayer = Vector3.Distance(transform.position, m_player.position); // ���� �÷��̾� ������ �Ÿ� ���
        if (distanceToPlayer <= listenRadius && audioSource.isPlaying)
        {
            return true;
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
    }z

    // ��� ó�� �޼���
    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject); // �� ������Ʈ �ı�
    }
}
