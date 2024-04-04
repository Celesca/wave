using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionPoint : MonoBehaviour
{
    public playerMovement pl;
    public Transform point;

    public float y = -0.1f;
    private void Awake()
    {
        pl = GetComponent<playerMovement>();
    }
    void Update()
    {
        if (pl.isCrouch)
        {
            point.position = new Vector2(point.position.x, y);
        }
        else
        {
            point.position = new Vector2(point.position.x, point.position.y);
        }
    }
}
