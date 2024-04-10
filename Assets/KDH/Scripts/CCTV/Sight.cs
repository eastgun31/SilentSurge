using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class Sight : MonoBehaviour
{
    public float radius = 5f;
    [Range(0,360)]
    public float angle;

    public bool detectC = true;
    public bool findT;

    Mesh viewMesh;
    public float meshResult;
    public MeshFilter meshFilter;

    public LayerMask playerM, etcM;

    public Collider isColP;

    public Transform visibleT;

    public RaycastHit hitR;

    Enemy enemy;

    public CCTVMovement cctv;
    
    public Vector3 playerpos;
    public Vector3 dir_T;
    public Transform detectTarget;

    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public struct Edge
    {
        public Vector3 pointA,pointB;
        public Edge(Vector3 _pointA, Vector3 _pointB)
        {
            pointA=_pointA;
            pointB=_pointB;
        }
    }
    public struct ViewInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float vangle;

        public ViewInfo(bool _hit, Vector3 _point, float _dst, float _vangle)
        {
            hit = _hit;                         // raycast가 힛 판정인지
            point = _point;                 // raycast의 마지막 도달 위치
            dst = _dst;                         // 
            vangle = _vangle;
        }
    }

    void Start()
    {
        GetComponent<Enemy>();
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        meshFilter.mesh = viewMesh;
        StartCoroutine(DetectDelay(0.2f));
    }

    private void LateUpdate()
    {
        Debug.Log(findT);
        DrawDetectArea();
    }

    IEnumerator DetectDelay(float delay)    // 탐지 딜레이 (0.2초)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            DetectTargets();
        }
    }

    void DetectTargets()
    {
        findT = false;
        Collider[] targets = Physics.OverlapSphere(transform.position, radius, playerM);  // radius(반지름) 내 원 영역의 playerM 콜라이더를 가져옴 
        for (int i = 0; i < targets.Length; i++)
        {
            detectTarget = targets[i].transform;
            dir_T = (detectTarget.position - transform.position).normalized;             // 타겟 위치 - 시야각 발사 위치 (정규화) = 타겟의 방향
            if (Vector3.Angle(transform.forward, dir_T) < angle / 2)                                        // 시야각의 정면과 타겟의 방향이 이루는 각도가 설정한 변수보다 작다면(안이라면)
            {
                float disT = Vector3.Distance(transform.position, detectTarget.position);       // 시야각 발사 위치 ~ 타겟 위치 = 타겟의 거리
                foreach (Collider col in targets)
                {
                    if (!Physics.Raycast(transform.position, dir_T, disT, etcM))                             // 타겟으로 가는 raycast에 장애물이 없다면
                    {
                        playerpos = dir_T;
                        findT = true;
                        detectTarget = visibleT;                                                                            //  detectTarget 은 플레이어
                        //if (EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
                        //{
                        //    EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level2;
                        //}
                        //if (EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level2)
                        //{
                        //    EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;
                        //}
                    }
                    //if (this.tag == "Enemy")    // 적이 플레이어 감지 후 탐지단계 상승
                    //{
                    //    if (EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
                    //    {
                    //        EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level2;
                    //    }
                    //    if (EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level2)
                    //    {
                    //        EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;
                    //    }
                    //}
                    if (this.name == "CCTV")    // CCTV가 플레이어 감지 후 탐지단계 상승
                    {
                        if(Physics.Raycast(transform.position, transform.forward, out hitR, radius, playerM))
                        {
                            if (EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
                            {
                                EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level2;
                            }
                            if (EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level2)
                            {
                                EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;
                            }
                        }
                    }
                }
            }
            else
            {
                findT = false;
                playerpos = Vector3.zero;
            }
        }
    }

    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal) // 범위 그려주기 위해 필요한 함수 (오일러 각을 3차원 벡터 값으로 변환)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }

    ViewInfo ViewObj(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, radius, etcM)) 
        {
            return new ViewInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewInfo(false, transform.position + dir * radius, radius, globalAngle);
        }
    }

    Edge FindEdge(ViewInfo minView, ViewInfo maxView)
    {
        float minAngle = minView.vangle;
        float maxAngle = maxView.vangle;
        Vector3 minPoint=Vector3.zero;
        Vector3 maxPoint=Vector3.zero;

        for(int i=0; i<edgeResolveIterations; i++)
        {
            float angle = minAngle + (maxAngle - minAngle) / 2;
            ViewInfo newViewCast= ViewObj(angle);
            bool edgeDstThresholdExceed = Mathf.Abs(minView.dst - newViewCast.dst) > edgeDstThreshold;
            if(newViewCast.hit==minView.hit && !edgeDstThresholdExceed)
            {
                minAngle = angle;
                minPoint= newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint= newViewCast.point;
            }
        }
        return new Edge(minPoint, maxPoint);
    }


    void DrawDetectArea()
    {
        int stepCount = Mathf.RoundToInt(angle * meshResult);
        float stepAngleSize = angle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewInfo prevViewCast = new ViewInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float dangle = transform.eulerAngles.y - angle / 2 + stepAngleSize * i;
            ViewInfo newViewinfo = ViewObj(dangle);

            if(i!=0)
            {
                bool edgeDstThresholdExceed = Mathf.Abs(prevViewCast.dst - newViewinfo.dst) > edgeDstThreshold;

                if(prevViewCast.hit != newViewinfo.hit || (prevViewCast.hit&&newViewinfo.hit&&edgeDstThresholdExceed))
                {
                    Edge e = FindEdge(prevViewCast, newViewinfo);

                    if(e.pointA!=Vector3.zero)
                    {
                        viewPoints.Add(e.pointA);
                    }
                    if(e.pointB!=Vector3.zero)
                    {
                        viewPoints.Add(e.pointB);
                    }
                }
            }
            viewPoints.Add(newViewinfo.point);
            prevViewCast = newViewinfo;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }
}
