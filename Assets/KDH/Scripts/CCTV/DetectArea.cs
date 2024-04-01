using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DetectArea : MonoBehaviour
{
    private Transform target;

    public float angleRange = 30f;
    public float radius = 3f;

    bool isCollision = false;

    Color red1 = new Color(0.75f, 0.17f, 0.12f, 0.2f);
    Color red2 = new Color(1f, 0f, 0f, 0.2f);

    void Start()
    {
        StartCoroutine(CCTV_Search_Movement(2f));
    }

    
    void Update()
    {
        
    }

    IEnumerator CCTV_Search_Movement(float delay)
    {
        yield return new WaitForSeconds(delay);
        Mathf.Lerp(-10f, 10f, 5f);
    }

    private void OnDrawGizmos()
    {
        Handles.color = isCollision ? red2 : red1;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}
