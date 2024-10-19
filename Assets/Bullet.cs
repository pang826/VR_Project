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
        if(collision.collider.tag == "Enemy")
        {
            enemy.SetActive(false);
        }
    }


}
