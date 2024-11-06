using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the gun attack mechanics, including normal attacks and skill attacks.
/// </summary>
public class gunATK : MonoBehaviour
{
    [SerializeField] private float atkCooldown;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private GameObject[] fireballs;

    [SerializeField] private float skillAtkCooldown;
    [SerializeField] private Transform skillPoint;
    [SerializeField] private GameObject[] skillFires;

    // Sound effect
    [SerializeField] private AudioSource AtkSoundEffect;

    private Animator anim;
    [SerializeField] private playerMovement playerMovement;

    private float cooldowntimer = Mathf.Infinity;

    public bool isSkillCooldown = false;
    private float skillCooldown;
    private int countSkillATK;

    public float timeSkillSMG = 5;
    public float cooldownTimeSkillSMG = 10.0f;

    public float timeAR;

    public playerMovement pl;

    // Cooldown for AR
    public float beforeShootTime = 0.5f;

    // Transform position Y if crouch
    private float y = -2.26f;

    /// <summary>
    /// Initializes the gun attack script.
    /// </summary>
    private void Start()
    {
    }

    /// <summary>
    /// Initializes the animator and player movement components.
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<playerMovement>();
    }

    /// <summary>
    /// Updates the gun attack mechanics, including handling input for normal and skill attacks.
    /// </summary>
    private void Update()
    {
        // Position point if crouch
        firepointPosition();
        skillpointPosition();
        beforeTimeAR();

        // Handle normal and skill attacks
        if (Input.GetKey(KeyCode.X) && atkCooldown < cooldowntimer)
        {
            // Skill attack
            if (isSkillCooldown)
            {
                // SMG
                if (pl.currentSwap == 1)
                {
                    attckingSkill(0.2f);
                }
                // AR
                else if (pl.currentSwap == 2 && beforeShootTime < cooldowntimer)
                {
                    StartCoroutine(ShootAR());
                }
            }
            // Normal attack
            else if (!isSkillCooldown)
            {
                // Pistol
                if (pl.currentSwap == 0)
                {
                    attckingNormal(0.1f);
                }
                // SMG
                else if (pl.currentSwap == 1)
                {
                    attckingNormal(0.2f);
                }
                // AR
                else if (pl.currentSwap == 2 && beforeShootTime < cooldowntimer)
                {
                    StartCoroutine(ShootAR());
                }
            }
        }

        // Check if click Z to use skill
        if (Input.GetKey(KeyCode.Z) && cooldownTimeSkillSMG <= 0 && pl.currentSwap != 0)
        {
            isSkillCooldown = true;
            cooldownTimeSkillSMG = 10;
        }

        cooldowntimer += Time.deltaTime;

        // SMG skill cooldown
        if (isSkillCooldown)
        {
            timeSkillSMG -= Time.deltaTime;

            if (timeSkillSMG <= 0)
            {
                isSkillCooldown = false;
                timeSkillSMG = 5;
            }
        }
        if (!isSkillCooldown)
        {
            cooldownTimeSkillSMG -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Handles the player's normal attack.
    /// </summary>
    /// <param name="cooldownTime">The cooldown time for the attack.</param>
    private void attckingNormal(float cooldownTime)
    {
        atkCooldown = cooldownTime;
        AtkSoundEffect.Play();
        Atk();
    }

    /// <summary>
    /// Handles the player's skill attack.
    /// </summary>
    /// <param name="cooldownTime">The cooldown time for the skill attack.</param>
    private void attckingSkill(float cooldownTime)
    {
        atkCooldown = cooldownTime;
        AtkSoundEffect.Play();
        skillATK();
    }

    /// <summary>
    /// Updates the bullet point position based on the player's crouch state.
    /// </summary>
    private void firepointPosition()
    {
        if (Input.GetKey(KeyCode.DownArrow) && playerMovement.isGrounded() && playerMovement.isCrouch)
        {
            bulletPoint.position = new Vector3(bulletPoint.position.x, -2.85f, bulletPoint.position.z);
        }
        else if (!playerMovement.isCrouch && playerMovement.isGrounded())
        {
            bulletPoint.position = new Vector3(bulletPoint.position.x, y, bulletPoint.position.z);
        }
    }

    /// <summary>
    /// Updates the skill point position based on the player's crouch state.
    /// </summary>
    private void skillpointPosition()
    {
        if (Input.GetKey(KeyCode.DownArrow) && playerMovement.isGrounded() && playerMovement.isCrouch)
        {
            skillPoint.position = new Vector3(skillPoint.position.x, -2.85f, skillPoint.position.z);
        }
        else if (!playerMovement.isCrouch && playerMovement.isGrounded())
        {
            skillPoint.position = new Vector3(skillPoint.position.x, y, skillPoint.position.z);
        }
    }

    /// <summary>
    /// Executes the normal attack.
    /// </summary>
    private void Atk()
    {
        anim.SetTrigger("atk");
        cooldowntimer = 0;

        fireballs[FindFireball()].transform.position = bulletPoint.position;
        fireballs[FindFireball()].GetComponent<fireballATK>().SetDirectionFireball(Mathf.Sign(transform.localScale.x));
    }

    /// <summary>
    /// Finds an inactive fireball to use for the attack.
    /// </summary>
    /// <returns>The index of the inactive fireball.</returns>
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    /// <summary>
    /// Executes the skill attack.
    /// </summary>
    private void skillATK()
    {
        anim.SetTrigger("atk");
        cooldowntimer = 0;

        skillFires[FindSkill()].transform.position = skillPoint.position;
        skillFires[FindSkill()].GetComponent<skillFire>().SetDirectionSkill(Mathf.Sign(transform.localScale.x));
    }

    /// <summary>
    /// Finds an inactive skill fireball to use for the skill attack.
    /// </summary>
    /// <returns>The index of the inactive skill fireball.</returns>
    private int FindSkill()
    {
        for (int i = 0; i < skillFires.Length; i++)
        {
            if (!skillFires[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    /// <summary>
    /// Handles the AR shooting rate.
    /// </summary>
    /// <returns>An IEnumerator for the coroutine.</returns>
    private IEnumerator ShootAR()
    {
        for (int i = 0; i < 3; i++)
        {
            AtkSoundEffect.Play();
            Atk();
            yield return new WaitForSeconds(0.11f);
        }
    }

    /// <summary>
    /// Updates the AR cooldown time.
    /// </summary>
    public void beforeTimeAR()
    {
        if (!isSkillCooldown && pl.currentSwap == 2)
        {
            beforeShootTime = 0.5f;
        }
        else if (isSkillCooldown && pl.currentSwap == 2)
        {
            beforeShootTime = 0.25f;
        }
    }

    /// <summary>
    /// Handles the AR attack.
    /// </summary>
    /// <param name="cooldown">The cooldown time for the AR attack.</param>
    public void shootingAR(float cooldown)
    {
        if (beforeShootTime <= 0f)
        {
            StartCoroutine(ShootAR());
            beforeShootTime = cooldown;
        }
        else
        {
            beforeShootTime -= Time.deltaTime;
        }
    }
}
