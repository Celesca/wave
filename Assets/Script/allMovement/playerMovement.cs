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
    [SerializeField] private AudioSource swapWeaponSoundEffect;
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
    private Animator anim;
    private Rigidbody2D body;

    public bool test = false;
    private GameObject go;

    // Dash Feature
    private bool canDash = true;
    private bool isDashing = false;
    private float dashPower = 70f;
    private float dashTime = 0.1f;
    private float dashCooldown = 1f;

    //Jump
    bool jump = true;

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




        /**
                if(horizontalInput != 0 && grounded)
                {
                    anim.SetBool("run", true);

                    if(isCrouch) 
                    {
                        anim.SetBool("run", false);
                        crouch();
                    }
                }
        **/


        //flip and Change Scale
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (isDashing)
            return;

        //Crouch
        crouch();

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Debug.Log("Dash");
            DashSoundEffect.Play();
            StartCoroutine(Dash());
        }

        // Jump
        if (isDoubleJump)
        {
            // Check if player is in the air (for double jump)
            if (isGrounded() && !Input.GetKey(KeyCode.Space))
            {
                canDoubleJump = false;
            }
            // double jump
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


        //Swap weapon
        if (Input.GetKeyDown(KeyCode.C))
        {
            swapWeaponSoundEffect.Play();
            swapWeapon();
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
        if (Input.GetKey(KeyCode.DownArrow))
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
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0;
        body.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    //swap weapon
    void swapWeapon()
    {
        if (currentSwap == 0)
        {
            currentSwap += 1;
            anim.SetLayerWeight(currentSwap - 1, 0);
            anim.SetLayerWeight(currentSwap, 1);
        }
        else
        {
            currentSwap -= 1;
            anim.SetLayerWeight(currentSwap + 1, 0);
            anim.SetLayerWeight(currentSwap, 1);
        }
    }

    public void enableDoubleJump()
    {
        isDoubleJump = true;
    }
}
