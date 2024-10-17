using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTimeSet : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] CharacterController character;
    [SerializeField] Vector3 previousPos;
    bool checkTime = false;
    [SerializeField] float playerSpeed;
    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }
    private void Update()
    {
        CheckSpeed();
        if(playerSpeed <= 0)
        {
            gameManager.FixedUpdateTimeSet(0.1f);
        }
        else if(playerSpeed >= 0.9f)
        {
            gameManager.FixedUpdateTimeSet(1f);
        }
        else
        {
            gameManager.FixedUpdateTimeSet(0.5f);
        }
        Debug.Log(playerSpeed);

    }
    private void FixedUpdate()
    {

    }
    void CheckSpeed()
    {
        if (!checkTime)
        {
            StartCoroutine(posCheck());
        }
        Vector3 curPos = character.transform.position;
        float Distance = Vector3.Distance(curPos, previousPos);
        playerSpeed = Distance / Time.deltaTime;
    }
    IEnumerator posCheck()
    {
        checkTime = true;
        previousPos = character.transform.position;
        yield return new WaitForSeconds(0.01f);
        checkTime = false;
        yield break;
    }
}
