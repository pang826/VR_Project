using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
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
