using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float curTime;
    [SerializeField] LineRenderer line;
    [SerializeField] GameObject enemy;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        curTime += Time.deltaTime;
        if(curTime >= 3)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 맞은 대상의 tag 가 Enemy 라면 반응
        if(collision.collider.CompareTag("Enemy"))
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
