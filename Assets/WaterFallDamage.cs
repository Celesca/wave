using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFallDamage : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }

    }
}
