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
        // ���� ����� tag �� Enemy ��� ����
        if (collision.collider.CompareTag("Enemy"))
        {
            // ���� �κ��� ������ٵ� �޾ƿ���
            Rigidbody targetRigid = collision.collider.GetComponent<Rigidbody>();
            // ���� ��� �޾ƿ���
            EnemyController enemy = collision.collider.GetComponentInParent<EnemyController>();
            // �ִϸ��̼� �ߴ�
            enemy.anim.enabled = false;
            // navMesh �ߴ�
            enemy.nav.enabled = false;
            // ���� �κ��� Ű�׸�ƽ �ߴ�
            targetRigid.isKinematic = false;
            // �Ѿ��� ���� ��ġ�������� ����� ����
            targetRigid.AddForce(transform.forward * 0.0001f, ForceMode.Impulse);

            // ���� ����� ��ü Ű�׸�ƽ �ߴ�
            enemy.Damage();
            // ������Ʈ �ı�
            Destroy(gameObject);
        }
    }
}
