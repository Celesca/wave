using UnityEngine;

public class fireballATK : MonoBehaviour
{
    [SerializeField] private float speed;//can set speed of fireball
    private float direction;
    private bool hit;

    private Animator animator;
    private BoxCollider2D boxCollider;

    public playerMovement pl;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;//speed of fireball
        transform.Translate(movementSpeed, 0, 0);
    }
    //expoleFireball if it hit other Collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("exp");

        //check if boxcollider hit tag 
        if (collision.tag == "Enemy")

        {
            if (pl.currentSwap == 0)
            {
                collision.GetComponent<Health>().TakeDamage(1);

            }
            else if (pl.currentSwap == 1)
            {

                collision.GetComponent<Health>().TakeDamage(0.5f);

            }
        }
    }
    //Set direction of fireball(left or right)
    public void SetDirectionFireball(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);
    }
    private void Deactivte()
    {
        gameObject.SetActive(false);    
    }
}
