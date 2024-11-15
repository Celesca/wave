using UnityEngine;

public class skillFire : MonoBehaviour
{
    [SerializeField] private float speed;//can set speed of skillFire
    private float direction;
    private bool hit;

    private Animator animator;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spd;

    [SerializeField] private swapGun swapGun;

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

        //Pistol
        if (swapGun.currentSwap == 1)
        {
            EnemyTakeDamage(collision, 1f);
        }
        //SMG
        if (swapGun.currentSwap == 2)
        {
            EnemyTakeDamage(collision, 1f);
        }
        //AR
        if (swapGun.currentSwap == 3)
        {
            EnemyTakeDamage(collision, 1f);
        }
    }
    //Set direction of skillFire(left or right)
    public void SetDirectionSkill(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            spd.flipX = true;
        else
        {
            spd.flipX = false;
        }
    }

    private void EnemyTakeDamage(Collider2D collision, float damageAmount)
    {
        collision.GetComponent<Health>().TakeDamage(damageAmount);
    }

    private void Deactivte()
    {
        gameObject.SetActive(false);
    }
}
