using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    Health health;
    playerMovement move;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ready for Collision");
        health = GetComponent<Health>();
        move = GetComponent<playerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AugmentBox"))
        {
            Debug.Log("Collided with: " + collision.gameObject.name);
            health.addHealth();
            health.addArmor();
            move.enableDoubleJump();

            Destroy(collision.gameObject, 0f);
        }
    }
}
