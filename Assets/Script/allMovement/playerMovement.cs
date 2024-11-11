using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private TrailRenderer tr;

    //SoundEffect
    [SerializeField] private AudioSource JumpSoundEffect;
    [SerializeField] private AudioSource DashSoundEffect;

    //fixUpdate for dash
    [HideInInspector] public Vector2 movementInput;
    [HideInInspector] public float acceleration;
    public Transform arrow;
    public float normalAcceleration;

    private float horizontalInput;
    public bool grounded;

    public bool isCrouch = false;

    public BoxCollider2D boxCollider;
    public Animator anim;
    private Rigidbody2D body;

    public bool test = false;
    private GameObject go;

    // Dash Feature
    private bool canDash = true;
    private bool isDashing = false;
    private float dashPower = 5f;
    private float dashTime = 0.1f;
    private float dashCooldown = 2f;

    //Jump
    bool jump = true;

    //Scale Player
    [SerializeField] private float xyz;

    //For swap weapon
    public int currentSwap;

    // Double Jump Call
    private bool isDoubleJump = false;
    private bool canDoubleJump;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        go = GetComponent<GameObject>();

        acceleration = normalAcceleration;
    }
    private void Update()
    {

        //Walk Left or right
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        //flip and Change Scale
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(xyz, xyz, xyz);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(xyz * -1f, xyz, xyz);
       


        if (isDashing)
            return;

        //Crouch
        crouch();

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Debug.Log("Dash");
            StartCoroutine(Dash());
            DashSoundEffect.Play();
        }

        // Jump
        if (isDoubleJump)
        {
            // Check if player is in the air (for double jump)
            if (isGrounded() && !Input.GetKey(KeyCode.Space))
            {
                canDoubleJump = false;
            }
            // Double jump
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded() || canDoubleJump)
                {
                    JumpSoundEffect.Play();
                    Jump();

                    canDoubleJump = !canDoubleJump;
                }
            }
        }
        else
        {
            // Normal jump
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isGrounded())
            {
                JumpSoundEffect.Play();
                Jump();
            }
        }

        if (boxCollider.gameObject.tag == "Augment")
        {
            test = true;
        }

        //Set yVelocity
        anim.SetFloat("yVelocity", body.velocity.y);

        //Animation run

        anim.SetBool("run", horizontalInput != 0 && grounded);
        anim.SetBool("grounded", grounded);


    }


    //Check anim working
    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }


    //FixUpdate
    private void FixedUpdate()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        body.velocity += movementInput * acceleration * Time.fixedDeltaTime;
    }




    //jump
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }

    //onGround
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }

    }

    //ground
    public bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    //wall
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    //Crouch
    public void crouch()
    {
        if (Input.GetKey(KeyCode.DownArrow) && (grounded == true))
        {
            isCrouch = true;
        }
        else
        {
            isCrouch = false;
        }
        anim.SetBool("crouch", isCrouch);
    }

    //Dash
    private IEnumerator Dash()
    {
        DashSoundEffect.Play();
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0;
        Vector3 dashPosition = transform.position + new Vector3(transform.localScale.x * dashPower, 0f, 0f);
        transform.position = dashPosition;
        Debug.Log("Dash posi: " + transform.position);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        Debug.Log("Dash");
        tr.emitting = false;
        body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        Debug.Log("CanDash");
        canDash = true;
    }
    /* Augment Call */

    public void enableDoubleJump()
    {
        isDoubleJump = true;
    }

    //set direction facing
    public Vector3 GetFacingDirection()
    {
        return transform.localScale.x > 0 ? Vector3.right : Vector3.left;
    }
}
