
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header ("Health")]
    [SerializeField]private float startingHealth;
    [SerializeField] private float armor;

	public float currentHealth { get; private set; }
	private Animator anim;
	private bool dead;

	[Header("iFrames")]
	[SerializeField]private float iFramesDuration;
	[SerializeField]private int numberOfFlashes;
	private SpriteRenderer spriteRend;
    private bool invulnerable;

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
        //For test damge
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }

    public void TakeDamage(float _damage)
		{
        if (invulnerable) return;
        //Armor
        if (armor > 0)
        {
            _damage = 0;
            armor -= 1;
            StartCoroutine(Invunerability());
        }

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
			

			if(currentHealth > 0)
			{
				anim.SetTrigger("hurt");
				StartCoroutine(Invunerability());
			}
			else
			{
				if(!dead)
				{
				anim.SetTrigger("die");
		
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
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(1, 0, true); //Layer number Player and Enemy
		for (int i = 0; i < numberOfFlashes; i++)
		{
			spriteRend.color = new Color(1,0,0,0.5f);
			yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes * 2));
		    spriteRend.color = Color.white;
			yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes * 2));
		}
		Physics2D.IgnoreLayerCollision(1, 0, false);//Layer number Player and Enemy
        invulnerable = false;
    }

	public void OnDestroy()
	{
		gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
