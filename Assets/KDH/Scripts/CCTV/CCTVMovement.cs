using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.ProBuilder.MeshOperations;

public class CCTVMovement : MonoBehaviour       // CCTV�� �Ӽ��� ������ ������, CCTV�� �þ߸� ���� ��� �ܰ踦 ������
{
    public enum cctv_state
    {
        cidle,                     // �Ϲ�
        detecting              // �÷��̾� ������
    }

    public cctv_state c_state;

    WaitForSeconds inReverse;                       // �÷��̾ �þ� ���� "���" ���� �� ��� �ܰ谡 �ö󰡴� ���ѽð�
    WaitForSeconds coolReverse;                   // �÷��̾ ������ ������ �� �����ð�
    WaitForSeconds wait;

    Sight csight;

    public bool canReverse;                    // �ܰ谡 �ö� �� �ִ� �����ð��� �������� (true)

    public float rotationSpeed;               // CCTV�� ȸ�� �ӵ�
    public float rotationAmount;            // �� �� ȸ���� ����
    public float rotationDuration;           // �� �� ȸ���� �ɸ��� �ð�

    private bool rotateClockwise = true;    // ȸ�� ����(�ð����)
    

    Quaternion rotationStartPos;                // CCTV�� ȸ�� ���� ����
    Quaternion rotationEndPos;                 // CCTV�� ȸ���� ����Ǵ� ����

    Quaternion formatStartPos;                  // �ʱ� ȸ�������� ���ư��� �����ϴ� ����
    Quaternion defaultPos;                         // �ʱ� ȸ����

    void Start()
    {
        defaultPos = transform.rotation;                           // defalultPos�� �ʱ� rotation �� ����
        rotationStartPos = transform.rotation;                  // rotationStartPos�� �ʱ� rotation �� ����

        StartCoroutine(AngleMove());                                // CCTV �¿� ȸ�� �ڷ�ƾ ȣ��

        inReverse = new WaitForSeconds(5f);                   // �þ� ���� "���" ���� �� ��� �ܰ谡 �ö󰡴� �ð� 5��
        coolReverse = new WaitForSeconds(15f);             // CCTV ���� �ð� 15��
        canReverse = true;                                                 // ���� �ð��� ������ �ܰ� ��ȯ ���� üũ
        csight = GetComponent<Sight>();
        StartCoroutine(CCTVStateCheck());                        // �þ� ���� ���� ������ CCTV�� ���¸� �������ִ� �ڷ�ƾ ȣ��
        c_state = cctv_state.cidle;                                       // CCTV�� �ʱ� ���´� �Ϲݻ���
    }

    void Update()
    {
        if (c_state == cctv_state.detecting)                          // �� �������� ���
        {
            StartCoroutine(DetectCCTVLevel());                     // ��跹���� �÷��ִ� �ڷ�ƾ ȣ��
        }
        //if (csight.findT)
        //{
        //    CCTVHomingPlayer();
        //}
        //if(!csight.findT) 
        //{
        //    CCTVFormatRotate();
        //}
    }

    void CCTVHomingPlayer()                         // �÷��̾ �þ߿� ������ �� �þ߿� ����� �÷��̾���� ���Ⱚ�� �̿��� �þ߰� ����
    {
        Quaternion homingRotation = Quaternion.LookRotation(csight.dir_T, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, homingRotation, 0.05f);
    }

    void CCTVFormatRotate()                         // �÷��̾ �þ߿� ����� �� �ʱ� ����� ������Ʈ�� ȸ������ �ҷ��� rotation �� �ʱ�ȭ
    {
        formatStartPos = transform.rotation;
        transform.rotation = Quaternion.Slerp(formatStartPos, defaultPos, 1f);
    }

    IEnumerator AngleMove()
    {
            while (true)
             {
                rotationStartPos = transform.rotation;                                                                           // �� �ʱ�ȭ�ϴ� ������ ȸ���� ���� �� ���� ȸ�� ���������� �ٲ�Ƿ�(=ȸ���� ������ �ٲ�Ƿ�)
                float targetAngle = rotateClockwise ? rotationAmount : -rotationAmount;                  // Clockwise�� True��  Amount(=�ð����) False��  -Amount(=�ݽð����)
                rotationEndPos = Quaternion.Euler(0, targetAngle, 0) * rotationStartPos;                    // ȸ�� ���� ������ rotationAmount(-rotationAmount) ������ �ʱ�ȭ ������
                                                                                                                                                       // Quaternion.Euler() ���Ϸ����� ���ʹϾ����� ��ȯ������

                float rotatingTime = 0f;                                                // ȸ������ �ð� �ʱ�ȭ
                while (rotatingTime < rotationDuration)                      // ȸ������ �ð��� �� ȸ�� �ð��� ���� �ʾ��� ��(=���� ȸ�� ���� ��)
                {
                    transform.rotation = Quaternion.Lerp(rotationStartPos, rotationEndPos, rotatingTime / rotationDuration);       // ȸ�� ������������ ������������ (ȸ������ �ð�) / (�� ȸ�� �ð�)���� �����ϸ� ȸ��
                    rotatingTime += Time.deltaTime;                             // ȸ������ �ð� �ֽ�ȭ
                    yield return null;                                                        // ȸ���� ���� �� ���� �ݺ����ֱ� ���ؼ� return null
                }
                rotateClockwise = !rotateClockwise;                             // ȸ���� �����ٸ� Clockwise�� bool ���� �ݴ�� ��ȯ
                yield return new WaitForSeconds(2f);                           // delay�� �ʰ� ������ �ڷ�ƾ ��ȣ��
             }
    }

    IEnumerator DetectCCTVLevel()           // �÷��̾ Ž�����϶� ��跹�� ���� (�����ð��� ������ ��, �þ� ���� 5�� �̻� ���� ���� ��)
    {
        if(canReverse)                                   // CCTV�� �ܰ� ��ȯ�� ������ �� (�����ð��� ������ ��)
        {
            if (c_state == cctv_state.detecting && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
            {
                canReverse = false;                                                                                                                                         // ��� �ܰ� ��ȯ �Ұ���
                EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level2;                                                                              // ��� �ܰ� 1 -> 2
                StartCoroutine(CCTVReverseCheck());                                                                                                            // ���� �ð� �ڷ�ƾ ȣ��
            }
            yield return inReverse;
            if (c_state == cctv_state.detecting && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level2)
            {
                canReverse = false;
                EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;                                                                              // ��� �ܰ� 2 -> 3
                StartCoroutine(CCTVReverseCheck());                                                                                                            // ���� �ð� �ڷ�ƾ ȣ��
            }
            yield return inReverse;
            if (c_state == cctv_state.detecting && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level3)
            {
                canReverse = false;
                EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;                                                                              // ��� �ܰ� 3 ����
                GameManager.instance.lv3PlayerPos = csight.detectTarget.position;
                StartCoroutine(CCTVReverseCheck());                                                                                                            // ���� �ð� �ڷ�ƾ ȣ��
            }
        }
    }

    IEnumerator CCTVStateCheck()                                // CCTV�� ���¸� �ٲ��ִ� �ڷ�ƾ
    {
        if (csight.findT)                                                       // ���� �þ� ���� ������ ��
        {
            c_state = cctv_state.detecting;                           // CCTV�� ���¸� detecting(�� ������)���� ��ȯ
        }
        else if(!csight.findT)                                                // ���� �þ߿��� ������� ��
        {
            c_state = cctv_state.cidle;                                   // CCTV�� ���¸� cidle(����)�� ��ȯ
        }
        yield return wait;
        StartCoroutine(CCTVStateCheck());                        // CCTV ���� üũ �ݺ�
    }

    IEnumerator CCTVReverseCheck()              // �����ð� 15�� �ڷ�ƾ
    {
        yield return coolReverse;                        // �����ð� 15�ʰ� ������
        canReverse = true;                                  // ��� �ܰ� ��ȯ ����
    }
}
