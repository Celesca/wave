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

        //ATK X normal and skill ATK
        if (Input.GetKey(KeyCode.X) && atkCooldown < cooldowntimer)
        {
            //Skill atk
            if (isSkillCooldown)
            {
                //SMG
                if (pl.currentSwap == 1)
                {
                    attckingSkill(0.2f);
                }
                //AR
                else if (pl.currentSwap == 2)
                {
                    attckingSkill(0.25f);
                }
               
            }
            //Normal atk
            else if (!isSkillCooldown)
            {
                //Pistol
                if (pl.currentSwap == 0)
                {
                    attckingNormal(0.5f);
                }
                //SMG
                else if (pl.currentSwap == 1)
                {
                    attckingNormal(0.2f);
                }
                //AR
                else if(pl.currentSwap == 2)
                {
                    attckingNormal(0.5f);
                }
            }
        }

        if (Input.GetKey(KeyCode.Z) && cooldownTimeSkillSMG <= 0)
        {
            isSkillCooldown = true;
            cooldownTimeSkillSMG = 10;
        }

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
    private void attckingNormal(float cooldownTime)
    {
        atkCooldown = cooldownTime;
        AtkSoundEffect.Play();
        Atk();
    }

    //cld atk normal
    private void attckingSkill(float cooldownTime)
    {
        atkCooldown = cooldownTime;
        AtkSoundEffect.Play();
        skillATK();
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