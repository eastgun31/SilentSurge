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

    public bool findT;

    Mesh viewMesh;
    public float meshResult;
    public MeshFilter meshFilter;

    public LayerMask playerM, etcM;

    public Collider isColP;

    public Transform visibleT;

    public RaycastHit hitR;

    Enemy enemy;
    
    public Vector3 playerpos;
    public Vector3 dir_T;
    public Transform detectTarget;

    public int edgeResolveIterations;
    public float edgeDstThreshold;

    [SerializeField]
    private int sightType;
    WaitForSeconds delay;

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
            hit = _hit;                         // raycast�� �� ��������
            point = _point;                 // raycast�� ������ ���� ��ġ
            dst = _dst;                         // 
            vangle = _vangle;
        }
    }

    private void OnEnable()
    {
        findT = false;
        //if(sightType ==2)
        //{
        //    enemy = GetComponent<Enemy>();
        //}
        delay = new WaitForSeconds(0.2f);
        StartCoroutine(DetectDelay());
    }

    void Start()
    {
        findT = false;
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        meshFilter.mesh = viewMesh;
        StartCoroutine(DetectDelay());
    }

    private void LateUpdate()
    {
        if(sightType == 1)                           // enemy �� Eo
            DrawDetectArea();
    }

    IEnumerator DetectDelay()    // Ž�� ������ (0.2��)
    {
        while(true)
        {
            yield return delay;
            DetectTargets();
            //if (sightType==1)
            //    DetectTargets();
            //else if(sightType==2)
            //{
            //    if (enemy.state != Enemy.EnemyState.die)
            //        DetectTargets();
            //    else if(enemy.state == Enemy.EnemyState.die)
            //        yield break;
            //}
                
        }
    }

    void DetectTargets()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, radius, playerM);  // radius(������) �� �� ������ playerM �ݶ��̴��� ������
        if(targets==null || targets.Length==0)
        {
            findT = false;
        }
            
        for (int i = 0; i < targets.Length; i++)
        {
            detectTarget = targets[i].transform;
            GameManager.instance.lv3PlayerPos = detectTarget.position;
            dir_T = (detectTarget.position - transform.position).normalized;             // Ÿ�� ��ġ - �þ߰� �߻� ��ġ (����ȭ) = Ÿ���� ����
            if (Vector3.Angle(transform.forward, dir_T) < angle / 2)                                        // �þ߰��� ����� Ÿ���� ������ �̷�� ������ ������ �������� �۴ٸ�(���̶��)
            {
                float disT = Vector3.Distance(transform.position, detectTarget.position);       // �þ߰� �߻� ��ġ ~ Ÿ�� ��ġ = Ÿ���� �Ÿ�
                foreach (Collider col in targets)
                {
                    if(GameManager.instance.isHide)
                    {
                        findT = false;
                    }
                    else if(!GameManager.instance.isHide && !Physics.Raycast(transform.position, dir_T,disT, etcM))
                    {
                        playerpos = dir_T;
                        findT = true;

                        if (sightType == 2 && GameManager.instance.playerchasing < 40)
                            GameManager.instance.playerchasing += 1;
                        else if (sightType == 1 && GameManager.instance.playerchasing < 40)
                            GameManager.instance.playerchasing += 10;

                        detectTarget = visibleT;                                                                            //  detectTarget �� �÷��̾�
                    }
                }
            }
            else
            {
                findT = false;
                //GameManager.instance.playerchasing = false;
                playerpos = Vector3.zero;
            }
        }
    }

    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal) // ���� �׷��ֱ� ���� �ʿ��� �Լ� (���Ϸ� ���� 3���� ���� ������ ��ȯ)
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
