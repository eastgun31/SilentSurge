using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CCTVMovement : MonoBehaviour       // CCTV�� Ž�� �ݰ��� �¿�� �ݺ������ִ� ��ũ��Ʈ
{
    private Transform target;

    public float rotationSpeed = 20f;       // ī�޶��� ȸ�� �ӵ�
    public float rotationAmount = 20f;      // �� �� ȸ���� ����
    public float rotationDuration = 2f;     // �� �� ȸ���� �ɸ��� �ð�

    private bool rotateClockwise = true;    // ȸ�� ����(�ð����)

    Quaternion startRotation;                  // ī�޶��� ȸ�� ���� ����
    Quaternion endRotation;                    // ī�޶��� ȸ���� ����Ǵ� ����

    public float angleRange = 30f;
    public float radius = 3f;


    bool isCollision = false;

    Color red1 = new Color(1f, 0f, 0f, 0.2f);
    Color red2 = new Color(0.75f, 0.17f, 0.12f, 0.2f);

    void Start()
    {
        startRotation = transform.rotation;     // ���� rotation ���� startRotation�� ������
        StartCoroutine(AngleMove(2f));          // �ڷ�ƾ ���� �Լ� (������ 2��)
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


    //private void OnDrawGizmos()
    //{
    //    Handles.color = isCollision ? red1 : red2;          // isCollision�� True�� red1, False�� red2
    //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);    // DrawSolidArc(������, ��ֺ���(��������), �׷��� ���� ����, ����, ������)
    //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    //}
}
