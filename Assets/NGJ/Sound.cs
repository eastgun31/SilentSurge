using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField]
     private Enemy enemyScript; // Enemy ��ũ��Ʈ�� ������ ������ ����
    SoundWallCheck check;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            Debug.Log("�߼Ҹ�");
            check = other.GetComponent<SoundWallCheck>();

            //Enemy ��ũ��Ʈ�� ChasePlayer �Լ� ȣ��
            if (enemyScript != null && check.canhear)
            {
                enemyScript.soundpos = other.gameObject.transform.position;
                enemyScript.m_triggered = true;
            }
            else
            {
                Debug.LogWarning("Enemy ��ũ��Ʈ.");

            }
        }
    }

    void Start()
    {
        enemyScript = transform.parent.GetComponent<Enemy>();

        // Enemy ��ũ��Ʈ�� ���� ��������
        //GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");

        //if (enemyObject != null)
        //{
        //    enemyScript = enemyObject.GetComponent<Enemy>();
        //}
        //else
        //{
        //    Debug.LogWarning("Enemy GameObject�� ã�� �� �����ϴ�.");
        //}
    }

    void Update()
    {

    }
}
