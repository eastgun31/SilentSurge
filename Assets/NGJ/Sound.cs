using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField]
     private Enemy enemyScript; // Enemy 스크립트의 참조를 저장할 변수

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            Debug.Log("발소리");

            //Enemy 스크립트의 ChasePlayer 함수 호출
            if (enemyScript != null)
            {
                enemyScript.soundpos = other.gameObject.transform.position;
                enemyScript.m_triggered = true;
            }
            else
            {
                Debug.LogWarning("Enemy 스크립트.");

            }
        }
    }

    void Start()
    {
        enemyScript = transform.parent.GetComponent<Enemy>();

        // Enemy 스크립트의 참조 가져오기
        //GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");

        //if (enemyObject != null)
        //{
        //    enemyScript = enemyObject.GetComponent<Enemy>();
        //}
        //else
        //{
        //    Debug.LogWarning("Enemy GameObject를 찾을 수 없습니다.");
        //}
    }

    void Update()
    {

    }
}
