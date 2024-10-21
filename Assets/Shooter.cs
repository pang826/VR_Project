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
        _bullet.GetComponent<Rigidbody>().velocity = dir * 10f;
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
                _bullet.GetComponent<Rigidbody>().velocity = dir * 10f;
                //Rigidbody rigid = _bullet.AddComponent<Rigidbody>();
                yield return new WaitForSeconds(0.1f);
                fireReady = true;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 맞은 대상의 tag 가 Enemy 라면 반응
        if (collision.collider.CompareTag("Enemy"))
        {
            // 맞은 부분의 리지드바디 받아오기
            Rigidbody targetRigid = collision.collider.GetComponent<Rigidbody>();
            // 맞은 대상 받아오기
            EnemyController enemy = collision.collider.GetComponentInParent<EnemyController>();
            // 애니메이션 중단
            enemy.anim.enabled = false;
            // navMesh 중단
            enemy.nav.enabled = false;
            // 맞은 부분의 키네마틱 중단
            targetRigid.isKinematic = false;
            // 총알을 맞은 위치에서부터 충격을 받음
            targetRigid.AddForce(transform.forward * 0.0001f, ForceMode.Impulse);

            // 맞은 대상의 전체 키네마틱 중단
            enemy.Damage();
            // 오브젝트 파괴
            Destroy(gameObject);
        }
    }
}
