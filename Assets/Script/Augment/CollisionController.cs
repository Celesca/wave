using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ready for Collision");
        health = GetComponent<Health>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AugmentBox"))
        {
            Debug.Log("Collided with: " + collision.gameObject.name);
            health.addArmor();
        }
    }
}
