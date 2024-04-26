using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    void Start()
    {
        Destroy(GameObject.Find("AudioManager"));
        Destroy(GameObject.Find("CanvasForPause"));
    }
}
