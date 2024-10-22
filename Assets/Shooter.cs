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
    bool isAttackable = false; // 던져서 공격이 되는지 여부
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

    // 플레이어가 집은 적 있을 때에만 던졌을 때 데미지를 줄 수 있도록
    public void PermitThrowAttack()
    {
        isAttackable = true;
        // 적의 손에 총이 있고 플레이어가 그 총을 빼앗을 경우 부모 관계 해제
        transform.SetParent(null);
    }

    public void ParentExit()
    {
        // 손을 떼었을 때 원래 부모관계로 돌아가는 것을 방지
        transform.SetParent(null);
        // 적의 손에 있던 총의 경우 kinematic이 true로 설정되어 있음
        // false로 전환해야 던질 수 있음
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 맞은 대상의 tag 가 Enemy 라면 반응
        if (collision.collider.CompareTag("Enemy") && isAttackable)
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

            // 맞은 대상의 전체 키네마틱 중단 + 사망상태 전환
            enemy.Damage();
            enemy.isDied = true;
            // 오브젝트 파괴
            Destroy(gameObject);
        }
    }
}
