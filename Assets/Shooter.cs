using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] Rigidbody bulletRigid;



    public void Shoot()
    {
        Instantiate(bullet, muzzlePoint.transform.position, muzzlePoint.transform.rotation);
    }
}
