using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] Transform tailPoint;
    Vector3 dir;
    bool isAutoFire = false;
    bool fireReady = true;
    private void Update()
    {
        dir = (muzzlePoint.position - tailPoint.position).normalized;
    }
    public void AutoFire()
    {
        isAutoFire = true;
        StartCoroutine(autoFire());
    }

    public void PistolFire()
    {
        GameObject _bullet = Instantiate(bullet, muzzlePoint.transform.position, transform.localRotation);
        _bullet.GetComponent<Rigidbody>().velocity = dir * 30f;
    }
    public void StopAutoFIre()
    {
        isAutoFire = false;
    }
    IEnumerator autoFire()
    {
        while(isAutoFire)
        {
            if(fireReady)
            {
                fireReady = false;
                GameObject _bullet = Instantiate(bullet, muzzlePoint.transform.position, transform.localRotation);
                _bullet.GetComponent<Rigidbody>().velocity = dir * 30f;
                //Rigidbody rigid = _bullet.AddComponent<Rigidbody>();
                yield return new WaitForSeconds(0.1f);
                fireReady = true;
            }
            yield return null;
        }
    }
}
