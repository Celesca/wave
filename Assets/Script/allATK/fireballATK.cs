using UnityEngine;

public class fireballATK : MonoBehaviour
{
    [SerializeField] private float speed;//can set speed of fireball
    private float direction;
    private bool hit;

    private Animator animator;
    private BoxCollider2D boxCollider;

    public playerMovement pl;

    // Augment
    private bool isColdWater = false;
    private bool isToxicWater = false;
    private bool isBurnWater = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Awake()
    {

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
            if (pl.currentSwap == 0)
            {
                HitEnemy(collision, 1f);
            }
            //SMG
            else if (pl.currentSwap == 1)
            {
                HitEnemy(collision, 0.5f);
            }
            //AR
            else if (pl.currentSwap == 2)
            {
                HitEnemy(collision, 1f);
            }
        }
    }

    private void HitEnemy(Collider2D collision, float damage)
    {
        GameObject gameObject = collision.gameObject;
        Health enemy = gameObject.GetComponent<Health>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);

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
                enemy.TakeDamage(0.5f);
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
