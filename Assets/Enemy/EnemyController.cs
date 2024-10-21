using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent nav;
    public Animator anim;
    [SerializeField] CharacterController player;

    [SerializeField] Rigidbody[] rigids;
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
        nav.destination = player.transform.position;
    }

    public void Damage()
    {
        foreach (Rigidbody rigid in rigids)
        {
            rigid.isKinematic = false;
        }
    }
}
