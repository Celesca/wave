using UnityEngine;

public class playerATK : MonoBehaviour
{
    [SerializeField] private float atkCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [SerializeField] private float skillAtkCooldown;
    [SerializeField] private Transform skillPoint;
    [SerializeField] private GameObject[] skillFires;

    //SoundEffect
    [SerializeField] private AudioSource AtkSoundEffect;

    private Animator anim;
    private playerMovement playerMovement;

    private float cooldowntimer = Mathf.Infinity;

    public bool isSkillCooldown = false;
    private float skillCooldown;
    private int countSkillATK;


    public float timeSkillSMG = 5;
    public float cooldownTimeSkillSMG = 10.0f;


    public playerMovement pl;

    //tranform position Y if Crouch
    private float y = -2.26f;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<playerMovement>();
    }
    private void Update()
    {
        //position point if crouch
        firepointPosition();
        skillpointPosition();


        if (Input.GetKey(KeyCode.X) && atkCooldown < cooldowntimer)
        {
            if (isSkillCooldown && pl.currentSwap == 1)
            {
                atkCooldown = 0.2f;
                AtkSoundEffect.Play();
                skillATK();
            }

            else if (!isSkillCooldown)
            {
                if (pl.currentSwap == 0)
                {
                    attcking(0.5f);
                }
                else if (pl.currentSwap == 1)
                {
                    attcking(0.2f);
                }
            }
        }

        if (Input.GetKey(KeyCode.Z) && cooldownTimeSkillSMG <= 0)
        {
            isSkillCooldown = true;
            cooldownTimeSkillSMG = 10;
        }

        /**
        if (Input.GetKey(KeyCode.Z) && skillAtkCooldown < cooldowntimer)
        {

            if (!isSkillCooldown)
            {
                skillATK();
                countSkillATK++;
            }
        }
        **/

        // Auto Update the skillAtkCooldown

        // Auto detect if player use skillATK 3 times


        cooldowntimer += Time.deltaTime;

        if (isSkillCooldown)
        {

            timeSkillSMG -= Time.deltaTime;

            Debug.Log(cooldownTimeSkillSMG);
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
    private void FixedUpdate()
    {

    }

    //cld atk normal
    private void attcking(float cooldownTime)
    {
        atkCooldown = cooldownTime;
        AtkSoundEffect.Play();
        Atk();
    }

    //position point
    //firepoint
    private void firepointPosition()
    {
        if (Input.GetKey(KeyCode.DownArrow) && playerMovement.isGrounded() && playerMovement.isCrouch)
        {
            firepoint.position = new Vector3(firepoint.position.x, -2.85f, firepoint.position.z);
        }
        else if (playerMovement.isCrouch == false && playerMovement.isGrounded())
        {
            firepoint.position = new Vector3(firepoint.position.x, y, firepoint.position.z);
        }
    }
    //skillpoint
    private void skillpointPosition()
    {
        if (Input.GetKey(KeyCode.DownArrow) && playerMovement.isGrounded() && playerMovement.isCrouch)
        {
            skillPoint.position = new Vector3(skillPoint.position.x, -2.85f, skillPoint.position.z);
        }
        else if (playerMovement.isCrouch == false && playerMovement.isGrounded())
        {
            skillPoint.position = new Vector3(skillPoint.position.x, y, skillPoint.position.z);
        }
    }

    //normalATK stop atk 
    private void Atk()
    {
        anim.SetTrigger("atk");
        cooldowntimer = 0;

        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<fireballATK>().SetDirectionFireball(Mathf.Sign(transform.localScale.x));
    }
    //Many fireball
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    //skillATK stop atk 
    private void skillATK()
    {
        anim.SetTrigger("atk");
        cooldowntimer = 0;

        skillFires[FindSkill()].transform.position = skillPoint.position;
        skillFires[FindSkill()].GetComponent<skillFire>().SetDirectionSkill(Mathf.Sign(transform.localScale.x));
    }
    //skillFireball
    private int FindSkill()
    {
        for (int i = 0; i < skillFires.Length; i++)
        {
            if (!skillFires[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}