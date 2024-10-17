using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public void UpdateTimeSet(float time)
    {
        Time.timeScale = time;
    }
    public void FixedUpdateTimeSet(float time)
    {
        Time.timeScale = time;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
