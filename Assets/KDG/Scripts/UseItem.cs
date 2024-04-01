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

    [SerializeField]
    private float throwpower = 10f;


    public void GunFire(Vector3 pos)
    {
        GameObject bullet = Instantiate(bulletPrefab, handGunModel.transform.position, handGunModel.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = pos * 1f;

        Destroy(bullet, 2.0f);
    }
    public void ThrowCoin()
    {
        GameObject coin = Instantiate(coinPrefab, throwposition.transform.position, Quaternion.identity);
        Rigidbody coinRigid = coin.GetComponent<Rigidbody>();

        coinRigid.AddForce(transform.forward * throwpower, ForceMode.Impulse);

    }
    public void ThrowFlashBang()
    {

    }

}
