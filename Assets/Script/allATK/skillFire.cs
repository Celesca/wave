using UnityEngine;

public class skillFire : MonoBehaviour
{
    [SerializeField] private float speed;//can set speed of skillFire
    private float direction;
    private bool hit;

    private Animator animator;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spd;

    public playerMovement pl;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spd = GetComponent<SpriteRenderer>(); 
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;//speed of skillfire
        transform.Translate(movementSpeed, 0, 0);
    }
    //expoleskillFire if it hit other Collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("exp");

        if (pl.currentSwap == 1)
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }
    //Set direction of skillFire(left or right)
    public void SetDirectionSkill(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
        /**
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;
        
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        **/
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            spd.flipX = true;
        else
        {
            spd.flipX = false;
        }
            


    }
    private void Deactivte()
    {
        gameObject.SetActive(false);
    }
}
