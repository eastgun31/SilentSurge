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
    public GameObject amJeon;
    public GameObject amSal;
    public Transform throwposition;
    public Transform bulletPos;
    public List<Material> mat = new List<Material>();
    public List<Material> mat2 = new List<Material>();

    [SerializeField]
    private GameObject map;
    private bool mapcheck = false;
    [SerializeField]
    private float throwpower = 3f;
    [SerializeField]
    private float throwpower2 = 4f;
    private string floor = "Floor";
    private string inroom = "InRoom";
    public bool heartCanUse = true;


    Vector3 angle;
    Vector3 pos;
    Ray ray;
    RaycastHit hit;
    int mask;
    LineRenderer drawLine;
    Item itemClass;
    CreateSound gunSound;
    SoundManager soundManager;
    GameManager gameManager;

    private void Start()
    {
        soundManager = SoundManager.instance;
        gameManager = GameManager.instance;
        itemClass = new Item();
        drawLine = GetComponent<LineRenderer>();
    }


    public void GunFire(Vector3 pos)
    {
        GameObject bullet = Instantiate(bulletPrefab, handGunModel.transform.position, Quaternion.identity);
        Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
        gunSound = bullet.GetComponent<CreateSound>();
        StartCoroutine(gunSound.SoundCreateDeleteGun());
        soundManager.EffectPlay(1, true, 0.4f);

        float zDeg = pos.z - bulletRigid.position.z;
        float xDeg = pos.x - bulletRigid.position.x;
        float rotDeg = -(Mathf.Rad2Deg * Mathf.Atan2(zDeg, xDeg) - 90);
        bulletRigid.MoveRotation(Quaternion.Euler(0, rotDeg, 0));
        bulletRigid.velocity = bulletPos.forward * 20f;
        gameManager.itemcount[0]--;

        Destroy(bullet, 2.0f);
    }

    public void ErageDraw()
    {
        //drawLine.positionCount = 0;
        drawLine.SetPosition(0, new Vector3(0, 0, 0));
        drawLine.SetPosition(1, new Vector3(0, 0, 0));
        //Destroy(drawLine);
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

    public void OnOffMap()
    {
        Debug.Log("111");
        if(!mapcheck)
        {
            Debug.Log("112");
            map.SetActive(true);
            mapcheck = true;
        }
        else if(mapcheck)
        {
            Debug.Log("113");
            map.SetActive(false);
            mapcheck = false;
        }
        
    }

    public IEnumerator ThrowCoin()
    {
       //Debug.Log("内风凭角青");

       yield return itemClass.animDelay;
       GameObject coin = Instantiate(coinPrefab, throwposition.transform.position, Quaternion.identity);
       Rigidbody coinRigid = coin.GetComponent<Rigidbody>();
       coinRigid.AddForce(angle * throwpower, ForceMode.Impulse);
        gameManager.itemcount[1]--;
        Destroy(coin, 5f);

        gameManager.canUse = false;
       yield return itemClass.itemcool;
        gameManager.canUse = true;
        StopAllCoroutines();
    }

    public IEnumerator ThrowFlashBang()
    {
        //Debug.Log("内风凭角青");
        gameManager.onecollison = true;
        yield return itemClass.animDelay;
        GameObject flashbang = Instantiate(flashbangModel, throwposition.transform.position, Quaternion.identity);
        Rigidbody flashbangRigid = flashbang.GetComponent<Rigidbody>();
        flashbangRigid.AddForce(angle * throwpower, ForceMode.Impulse);
        gameManager.itemcount[2]--;
        Destroy(flashbang, 5f);

        gameManager.canUse = false;
        yield return itemClass.itemcool;
        gameManager.canUse = true;
        StopAllCoroutines();
    }

    public IEnumerator HeartSee()
    {
        heartCanUse = false;
        heartseeCam.SetActive(true);
        yield return itemClass.heartseeDuration;
        heartseeCam.SetActive(false);
        yield return itemClass.heartseeDuration;
        heartCanUse = true;
    }

    public IEnumerator Assassination()
    {
        amJeon.SetActive(true);
        amSal.SetActive(true);
        soundManager.EffectPlay(3, true, 1f);
        yield return itemClass.amJeonDelay;
        amSal.SetActive(false);
        amJeon.SetActive(false);
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
