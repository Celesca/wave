using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
    public float moveX;
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;

    public bool isJumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");//-1=>1
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        }

    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

    }
    private void OnCollisionExit2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Item"))
        {
            //ลบ item
            Destroy(target.gameObject);
        }
        if (target.gameObject.CompareTag("Enemy"))
        {

        }
    }



}
