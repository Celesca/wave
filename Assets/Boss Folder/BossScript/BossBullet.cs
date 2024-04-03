using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public bool isFlipped = false;
    public float speed = 2.5f;
    private Rigidbody2D rb;
    public float damage;
    private Transform limit;

    // Start is called before the first frame update
    void Start()
    {
        limit = GameObject.FindGameObjectWithTag("Limit").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = new Vector2(limit.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        if (collision.tag == "Limit")
        {
            Destroy(gameObject);
        }
    }
}

