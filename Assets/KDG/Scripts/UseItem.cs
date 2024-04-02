using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject handGunModel;
    public GameObject coinPrefab;
    public GameObject flashbangModel;
    public Transform throwposition;
    public List<Material> mat = new List<Material>();
    public List<Material> mat2 = new List<Material>();

    [SerializeField]
    private float throwpower = 3f;
    private string floor = "Floor";

    Color color1 = new Color(1, 0, 0);

    Vector3 angle;
    Vector3 pos;
    Ray ray;
    RaycastHit hit;
    int mask;
    LineRenderer drawLine;
    Camera cam;

    private void Start()
    {
        drawLine = GetComponent<LineRenderer>();
        cam = Camera.main;
    }


    public void GunFire(Vector3 pos)
    {
        GameObject bullet = Instantiate(bulletPrefab, handGunModel.transform.position, Quaternion.identity);
        Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();

        float zDeg = pos.z - bulletRigid.position.z;
        float xDeg = pos.x - bulletRigid.position.x;
        float rotDeg = -(Mathf.Rad2Deg * Mathf.Atan2(zDeg, xDeg) - 90);
        bulletRigid.MoveRotation(Quaternion.Euler(0, rotDeg, 0));
        bulletRigid.AddForce(new Vector3(pos.x,0,pos.z) * 5f, ForceMode.Impulse);

        Destroy(bullet, 2.0f);
    }

    public void ThrowPosition(bool a, bool b)
    {
        pos = Input.mousePosition;
        pos.z = Camera.main.farClipPlane;

        ray = Camera.main.ScreenPointToRay(pos);
        mask = LayerMask.GetMask(floor);

        if(Physics.Raycast(ray, out hit,100f, mask))
        {
            drawLine.SetPosition(0, transform.position + new Vector3(0,0.2f,0));
            drawLine.SetPosition(1, hit.point);
            if(a)
                drawLine.SetMaterials(mat);
            else if(b)
                drawLine.SetMaterials(mat2);
            angle = hit.point - transform.position;

            angle.y = 3.7f;
        }
    }

    public void ThrowCoin()
    {
        GameObject coin = Instantiate(coinPrefab, throwposition.transform.position, Quaternion.identity);
        Rigidbody coinRigid = coin.GetComponent<Rigidbody>();

        //angle.y = 2f;
        coinRigid.AddForce(angle * throwpower, ForceMode.Impulse);

        Destroy(coin, 5f);
    }

    public void ThrowFlashBang()
    {
        GameObject flashbang = Instantiate(flashbangModel, throwposition.transform.position, Quaternion.identity);
        Rigidbody flashbangRigid = flashbang.GetComponent<Rigidbody>();

        flashbangRigid.AddForce(angle * throwpower, ForceMode.Impulse);

        Destroy(flashbang, 5f);
    }

}
