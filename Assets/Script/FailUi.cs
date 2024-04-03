using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailUi : MonoBehaviour
{
    public GameObject failUi;

    public void Fail()
    {
        failUi.SetActive(true);
        Time.timeScale = 0f;
    }
}
