using UnityEngine;

public class playerMovement : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    //fixUpdate for dash
    [HideInInspector] public Vector2 movementInput;
    [HideInInspector] public float acceleration;
    public Transform arrow;
    public float normalAcceleration;

    private float horizontalInput;
    private bool grounded;

    public bool isCrouch;

    private BoxCollider2D boxCollider;
    private Animator anim; 
    private Rigidbody2D body;
    public float rightx,y,z;
    public float leftx;
    

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        acceleration = normalAcceleration;
    }
    private void Update()
    {
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //arrow.up = (mousePos - (Vector2)transform.position).normalized;

        //เดินซ้าย-ขวา
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, body.velocity.y);

        //flip and Change Scale
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && grounded)
            Jump();

        crouch();

        //Animation run
        anim.SetBool("run", horizontalInput != 0 && grounded);
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
        grounded = false;
    }
    //onGround
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
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
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    //Crouch
    public void crouch()
    {
        if(Input.GetKey(KeyCode.DownArrow)) {
            isCrouch = true;
        }
        else
        {
            isCrouch = false;
        }
        anim.SetBool("crouch", isCrouch);
    }

}
