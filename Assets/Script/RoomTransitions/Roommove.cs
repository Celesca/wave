using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roommove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private mainCamera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<mainCamera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cam.maxPos = cameraChange;
            cam.minPos = cameraChange;
            collision.transform.position += playerChange;
        }
    }
}
