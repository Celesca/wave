using System.Collections;
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

    private bool isRight = true;
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

    //Scale Player
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z; 

    //Jump
    bool jump = true;

    //For swap weapon
    public int currentSwap;

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
            transform.localScale = new Vector3(x, y, z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(x * -1f, y, z);
        
        if (isDashing)
                return;

        //Crouch
        crouch();

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
                Debug.Log("Dash");
                StartCoroutine(Dash());
        }

        //Jump
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)) && grounded)
        {
            JumpSoundEffect.Play();
            Jump();
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

        anim.SetBool("run", horizontalInput != 0  &&  grounded);
        anim.SetBool("grounded", grounded);

        
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
            grounded = true;
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
        DashSoundEffect.Play();
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
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
        }
        else if(currentSwap == 1)
        {
            currentSwap += 1;
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(2, 1);
            anim.SetLayerWeight(3, 0);
        }
        else if (currentSwap == 2)
        {
            currentSwap += 1;
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(3, 1);
        }
        else if (currentSwap == 3)
        {
            currentSwap -= 3;
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(0, 0);
        }
    }

}
