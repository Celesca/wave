using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyObj : MonoBehaviour {
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
