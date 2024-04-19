using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CCTVMovement : MonoBehaviour       // CCTV�� Ž�� �ݰ��� �¿�� �ݺ������ִ� ��ũ��Ʈ
{
    public enum cctv_state
    {
        cidle,
        detecting              // ����
    }

    public cctv_state c_state;

    WaitForSeconds cctv_elevel_reverse;             // enemylv ��� �ð�
    WaitForSeconds onReverse;                          // �÷��̾ ������ ������ �� �����ð�
    WaitForSeconds wait;

    Sight csight;

    public bool isDetecting;
    public bool canReverse;

    public float rotationSpeed;           // ī�޶��� ȸ�� �ӵ�
    public float rotationAmount;        // �� �� ȸ���� ����
    public float rotationDuration;         // �� �� ȸ���� �ɸ��� �ð�

    private bool rotateClockwise = true;    // ȸ�� ����(�ð����)

    Quaternion startRotation;                   // ī�޶��� ȸ�� ���� ����
    Quaternion endRotation;                    // ī�޶��� ȸ���� ����Ǵ� ����

    //public float angleRange = 30f;
    //public float radius = 3f;

    bool isCollision = false;

    Color red1 = new Color(1f, 0f, 0f, 0.2f);
    Color red2 = new Color(0.75f, 0.17f, 0.12f, 0.2f);

    void Start()
    {
        startRotation = transform.rotation;     // ���� rotation ���� startRotation�� ������
        StartCoroutine(AngleMove(2f));          // �ڷ�ƾ ���� �Լ� (������ 2��)
        cctv_elevel_reverse = new WaitForSeconds(5f);
        onReverse = new WaitForSeconds(15f);
        canReverse = true;
        csight = GetComponent<Sight>();
        StartCoroutine(CCTVStateCheck());
        c_state = cctv_state.cidle;
    }

    void Update()
    {
        if(c_state == cctv_state.detecting)
        {
            StartCoroutine(DetectCCTVLevel());
        }
    }

    
    IEnumerator AngleMove(float delay)
    {
        while(true)
        {
            startRotation = transform.rotation;   // �� �ʱ�ȭ�ϴ� ������ ȸ���� ������ ���� ���������� �ٲ�Ƿ�(=ȸ���� ������ �ٲ�Ƿ�)

            float targetAngle = rotateClockwise ? rotationAmount : -rotationAmount;     // Clockwise�� True��  Amount(=�ð����) False��  -Amount(=�ݽð�)
            endRotation = Quaternion.Euler(0, targetAngle, 0) * startRotation;                 // y�� �ش��ϴ� ���⿡ �°� ȸ��          // *startRotation ??????
                                                                                                                                       //Quaternion.Euler(x�� ȸ��, y�� ȸ��, z�� ȸ��)

            float rotatingTime = 0f;                                            // ȸ������ �ð� �ʱ�ȭ
            while(rotatingTime<rotationDuration)                     // ȸ������ �ð��� �� ȸ�� �ð��� ���� �ʾ��� ��(=���� ȸ�� ���� ��)
            {
                transform.rotation=Quaternion.Lerp(startRotation, endRotation,rotatingTime/rotationDuration);       // ȸ�� ���� �������� ������������ ȸ���� "ȸ������ �ð�"/"�� ȸ�� �ð�"���� ��������
                rotatingTime += Time.deltaTime;                         // ȸ������ �ð� �ֽ�ȭ
                yield return null;                                                    // ȸ���� ���� �� ���� �ݺ����ֱ� ���ؼ� return null
            }
            rotateClockwise = !rotateClockwise;                         // ȸ���� �����ٸ� Clockwise�� bool ���� �ݴ�� ��ȯ
            yield return new WaitForSeconds(delay);                  // delay�� �ʰ� ������ �ڷ�ƾ �����
        }
    }

    IEnumerator DetectCCTVLevel()           // �÷��̾ Ž�����϶� ��跹��+1 (ó�� �����ϰų�, �����ð��� ������ ��)
    {
        if(canReverse) 
        {
            if (c_state == cctv_state.detecting && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
            {
                canReverse = false;
                //GameManager.instance.playerchasing = true;
                EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level2;
                Debug.Log("2");
                StartCoroutine(CCTVReverseCheck());
            }
            yield return cctv_elevel_reverse;
            if (c_state == cctv_state.detecting && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level2)
            {
                canReverse = false;
                //GameManager.instance.playerchasing = true;
                EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;
                Debug.Log("3");
                StartCoroutine(CCTVReverseCheck());
            }
            yield return cctv_elevel_reverse;
            if (c_state == cctv_state.detecting && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level3)
            {
                canReverse = false;
                EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;
               // GameManager.instance.playerchasing = true;
                Debug.Log("333");
                GameManager.instance.lv3PlayerPos = csight.detectTarget.position;
                StartCoroutine(CCTVReverseCheck());
            }
            isDetecting = false;
        }
    }

    IEnumerator CCTVStateCheck()
    {
        if (csight.findT)                                                       // ���� ó�� ������ ��
        {
            c_state = cctv_state.detecting;
            isDetecting = true;
        }
        else if(!csight.findT)
        {
            c_state = cctv_state.cidle;
        }
        yield return wait;
        StartCoroutine(CCTVStateCheck());
    }

    IEnumerator CCTVReverseCheck()              // �����ð� 15�� �� �ܰ����� �����ϰԲ�
    {
        yield return onReverse;
        canReverse = true;
    }


    //private void OnDrawGizmos()
    //{
    //    Handles.color = isCollision ? red1 : red2;          // isCollision�� True�� red1, False�� red2
    //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);    // DrawSolidArc(������, ��ֺ���(��������), �׷��� ���� ����, ����, ������)
    //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    //}
}
