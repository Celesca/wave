using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnter : MonoBehaviour
{
    public Vector2 camMax;
    public Vector2 camMin;
    public Vector3 playerChange;
    private mainCamera cam;
    public BoxCollider2D boxCollider;

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
            cam.maxPos = camMax;
            cam.minPos = camMin;
            collision.transform.position += playerChange;
            boxCollider.isTrigger = false;
        }
    }
}
