using UnityEngine;

public class fireballATK : MonoBehaviour
{
    [SerializeField] private float speed;//can set speed of fireball
    private float direction;
    private bool hit;

    private Animator animator;
    private BoxCollider2D boxCollider;

    [SerializeField] private swapGun swapGun;

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
            //Damage
            //Pistol
            if (swapGun.currentSwap == 0)
            {
                EnemyTakeDamage(collision, 1f);
            }
            //SMG
            else if (swapGun.currentSwap == 1)
            {
                EnemyTakeDamage(collision, 0.5f);
            }
            //AR
            else if (swapGun.currentSwap == 2)
            {
                EnemyTakeDamage(collision, 1f);
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
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivte()
    {
        gameObject.SetActive(false);
    }

    private void EnemyTakeDamage(Collider2D collision, float damageAmount)
    {
        collision.GetComponent<Health>().TakeDamage(damageAmount);

        if (isColdWater)
        {
            // Slow enemy
        }

        if (isToxicWater)
        {
            // Enemy takes 0.5 damage for 2 seconds
        }

        if (isBurnWater)
        {
            // Enemy take extra damages
        }
    }

    // Augment Call
    public void ActivateColdWater()
    {
        isColdWater = true;
    }

    public void ActivateToxicWater()
    {
        isToxicWater = true;
    }

    public void ActivateBurningWater()
    {
        isBurnWater = true;
    }
}
