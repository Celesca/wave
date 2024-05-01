using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("Health")]
    [SerializeField] public float startingHealth;

    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    public BossSpecial bossSpecial;
    public BossMove1 bossMove1;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {

        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            TakeDamage(1);
    }

    public void TakeDamage(float _damage)
    {
        //Armor

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        
        
        if (currentHealth > 0)
        {
            StartCoroutine(Invunerability());
            if (currentHealth == startingHealth / 2)
            {
                anim.SetTrigger("Hurt");
                StartCoroutine(Invunerability());
            }
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("Die");

                foreach (Behaviour component in components)
                    component.enabled = false;
                dead = true;
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(1, 0, true); //Layer number Player and Enemy
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(1, 0, false);//Layer number Player and Enemy
    }

    public void OnDestroy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
