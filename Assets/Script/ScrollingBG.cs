using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    public float speed = 1f;
    private Vector3 StartPosition;
    private float repeatWidth;
    void Start()
    {
        StartPosition = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < StartPosition.x - repeatWidth)
        {
            transform.position = StartPosition;
        }
    }
}