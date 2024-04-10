using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using ItemInfo;

public class UseItem : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject handGunModel;
    public GameObject coinPrefab;
    public GameObject flashbangModel;
    public GameObject heartseeCam;
    public Transform throwposition;
    public Transform bulletPos;
    public List<Material> mat = new List<Material>();
    public List<Material> mat2 = new List<Material>();

    [SerializeField]
    private float throwpower = 3f;
    private string floor = "Floor";
    private string inroom = "InRoom";
    //private bool canUse = true;

    WaitForSeconds wait;
    Vector3 angle;
    Vector3 pos;
    Ray ray;
    RaycastHit hit;
    int mask;
    LineRenderer drawLine;
    Item itemClass;
    Camera cam;

    private void Start()
    {
        itemClass = new Item();
        wait = new WaitForSeconds(0.5f);
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
        bulletRigid.velocity = bulletPos.forward * 20f;
        GameManager.instance.itemcount[0]--;

        Destroy(bullet, 2.0f);
    }

    public void ErageDraw()
    {
        drawLine.positionCount = 0;
    }

    public void ThrowPosition(bool a, bool b)
    {
        drawLine.positionCount = 2;
        pos = Input.mousePosition;
        pos.z = Camera.main.farClipPlane;

        ray = Camera.main.ScreenPointToRay(pos);
        mask = LayerMask.GetMask(floor) | LayerMask.GetMask(inroom);

        if(Physics.Raycast(ray, out hit,100f, mask))
        {
            drawLine.SetPosition(0, transform.position + new Vector3(0,0.2f,0));
            drawLine.SetPosition(1, hit.point + new Vector3(0, 0.2f, 0));
            if(a)
                drawLine.SetMaterials(mat);
            else if(b)
                drawLine.SetMaterials(mat2);
            angle = hit.point - transform.position;

            angle.y = 3.7f;
        }
    }

    public IEnumerator ThrowCoin()
    {
       //Debug.Log("内风凭角青");

       yield return itemClass.animDelay;
       GameObject coin = Instantiate(coinPrefab, throwposition.transform.position, Quaternion.identity);
       Rigidbody coinRigid = coin.GetComponent<Rigidbody>();
       coinRigid.AddForce(angle * throwpower, ForceMode.Impulse);
        GameManager.instance.itemcount[1]--;
        Destroy(coin, 5f);

        GameManager.instance.canUse = false;
       yield return itemClass.itemcool;
        GameManager.instance.canUse = true;
        StopAllCoroutines();
    }

    public IEnumerator ThrowFlashBang()
    {
        //Debug.Log("内风凭角青");

        yield return itemClass.animDelay;
        GameObject flashbang = Instantiate(flashbangModel, throwposition.transform.position, Quaternion.identity);
        Rigidbody flashbangRigid = flashbang.GetComponent<Rigidbody>();
        flashbangRigid.AddForce(angle * throwpower, ForceMode.Impulse);
        GameManager.instance.itemcount[2]--;
        Destroy(flashbang, 5f);

        GameManager.instance.canUse = false;
        yield return itemClass.itemcool;
        GameManager.instance.canUse = true;
        StopAllCoroutines();
    }

    public IEnumerator HeartSee()
    {
        heartseeCam.SetActive(true);

        yield return itemClass.heartseeDuration;

        heartseeCam.SetActive(false);
    }

    //public void ThrowCoin()
    //{
    //    GameObject coin = Instantiate(coinPrefab, throwposition.transform.position, Quaternion.identity);
    //    Rigidbody coinRigid = coin.GetComponent<Rigidbody>();

    //    //angle.y = 2f;
    //    coinRigid.AddForce(angle * throwpower, ForceMode.Impulse);

    //    Destroy(coin, 5f);
    //}

    //public void ThrowFlashBang()
    //{

    //    GameObject flashbang = Instantiate(flashbangModel, throwposition.transform.position, Quaternion.identity);
    //    Rigidbody flashbangRigid = flashbang.GetComponent<Rigidbody>();
    //    flashbangRigid.AddForce(angle * throwpower, ForceMode.Impulse);
    //    Destroy(flashbang, 5f);
    //}

}
