using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent nav;
    public Animator anim;
    [SerializeField] CharacterController player;

    [SerializeField] Rigidbody[] rigids;

    public bool isDied = false;
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        player = FindAnyObjectByType<CharacterController>();

        rigids = GetComponentsInChildren<Rigidbody>();

        // 물리충격 없음
        foreach(Rigidbody rigid in  rigids)
        {
            rigid.isKinematic = true;
        }
    }
    private void Update()
    {
        if (!isDied)
        {
            nav.destination = player.transform.position;
        }
        if(isDied)
        {
            StartCoroutine(Die());
        }
    }

    public void Damage()
    {
        foreach (Rigidbody rigid in rigids)
        {
            rigid.isKinematic = false;
        }
    }

    IEnumerator Die()
    {
        isDied = false; // 코루틴의 무한반복을 막기 위함
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
