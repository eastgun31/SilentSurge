using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField]
     private Enemy enemyScript; // Enemy ��ũ��Ʈ�� ������ ������ ����

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            Debug.Log("�߼Ҹ�");

            //Enemy ��ũ��Ʈ�� ChasePlayer �Լ� ȣ��
            if (enemyScript != null)
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
