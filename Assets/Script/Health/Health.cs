using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using FirstGearGames.SmoothCameraShaker;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    [SerializeField] private float armor;
    public ShakeData shakeData;
    private bool isDead;

    // connect to GameManagerScript Class
    public GameManagerScript GameManager;

    public float currentHealth { get; private set; }
    private Animator anim;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    private bool isInvulnerable = false;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        if (armor > 0)
        {
            spriteRend.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }

    private void Update()
    {
        // For test damage
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }

    public void TakeDamage(float _damage)
    {
        // Armor
        if (armor > 0)
        {
            _damage = 0;
            armor -= 1;
            StartCoroutine(Invunerability());
        }

        if (!isInvulnerable)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

            if (currentHealth <= 0 && !isDead)
            {
                CameraShakerHandler.Shake(shakeData);
                anim.SetTrigger("die");
                GetComponent<playerMovement>().enabled = false;
                isDead = true;
                StartCoroutine(DieDelay()); // start delay player dead animation
            }
            else
            {
                anim.SetTrigger("hurt");
                StartCoroutine(Invunerability());
                CameraShakerHandler.Shake(shakeData);
            }
        }
    }

    public void FailedUI()
    {
        GameManager.gameOver(); // show Failed UI after die
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator DieDelay()
    {
        // WaitForSeconds() <-- after die delay
        yield return new WaitForSeconds(1.5f);
        FailedUI(); // show Failed UI after die
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(1, 0, true); // Layer number Player and Enemy
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(1, 0, false); // Layer number Player and Enemy
    }

    /* Augment Call */

    public void healthBoost()
    { 
        startingHealth = 4;
        currentHealth = 4;
    }

    public void armorBoost()
    {
        armor = 1;
    }
}
