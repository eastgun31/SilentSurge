using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    NavMeshAgent m_enemy;
    [SerializeField] Vector3[] customDestinations; // �� ĳ���Ͱ� �̵��� ���������� ��ǥ�� �����մϴ�.
    int m_currentDestinationIndex = 0;
    Transform m_player;
    bool m_followingPlayer = false; // �� ĳ���Ͱ� �÷��̾ ���� ������ ���θ� ��Ÿ���ϴ�.


    public bool m_triggered = false; // Ʈ���� �浹 ���θ� ��Ÿ���ϴ�.
    public Vector3 soundpos;

    int dLevel = 1;

    [SerializeField] private float stoppingDistance = 1f; // ���� ���� �Ÿ�

    public event Action<Player> PlayerSpotted; // �÷��̾� �߰� �� �̺�Ʈ


    // ü�� ���� ����
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

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
        if (CanSeePlayer())
        {
            m_followingPlayer = true;
            m_enemy.SetDestination(m_player.position); // �÷��̾ �߰��ϸ� �÷��̾ ���󰩴ϴ�.

            // �÷��̾ �߰����� �� �̺�Ʈ �߻�
            PlayerSpotted?.Invoke(m_player.GetComponent<Player>());
        }
        else if (m_triggered)
        {
            m_enemy.SetDestination(soundpos); // Ʈ���� �浹 �� �÷��̾ ���󰩴ϴ�.
        }
        else
        {
            m_followingPlayer = false;

            if (!m_enemy.pathPending && m_enemy.remainingDistance < stoppingDistance)
            {
                SetNextDestination(); // ���ÿ��� �⺻���� �������� �̵��մϴ�.
            }
        }
    }
    public void ChasePlayer(Vector3 position)
    {
        m_enemy.SetDestination(position);


    }

    void SetNextDestination()
    {
        if (!m_followingPlayer && customDestinations.Length > 0)
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            m_triggered = true; // Ʈ���� �浹 �� ���¸� �����մϴ�.
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sound"))
        {
            // ��ֹ��� �����ϸ� �������� �ٽ� �����մϴ�.
            SetNextDestination();
        }

        // �Ѿ� ����� �浹�̸鼭 �÷��̾� �Ѿ��� ��쿡�� ���ظ� �޽��ϴ�.
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10); // 10�� �������� �Խ��ϴ�.
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die(); // ü���� 0 ���ϰ� �Ǹ� ��� ó���մϴ�.
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject); // �� ������Ʈ�� �ı��մϴ�.
    }
}
