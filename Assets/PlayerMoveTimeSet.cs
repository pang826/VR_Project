using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTimeSet : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] CharacterController character;
    [SerializeField] Vector3 previousPos;
    [SerializeField] float playerSpeed;
    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void Start()
    {
        InvokeRepeating("CheckSpeed", 0, 0.02f);
    }

    private void Update()
    {
        if (playerSpeed <= 0f)
        {
            gameManager.FixedUpdateTimeSet(0.1f);
        }
        else
        {
            gameManager.FixedUpdateTimeSet(1f);
        }
    }
    void CheckSpeed()
    {
        Vector3 curPos = character.transform.position;
        float Distance = Vector3.Distance(curPos, previousPos);
        playerSpeed = Distance / 0.02f;

        previousPos = curPos;
    }
}
