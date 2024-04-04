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

    private bool isSkillCooldown;
    private float skillCooldown;
    private int countSkillATK;


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
            AtkSoundEffect.Play();
            Atk();
        }
        if (Input.GetKey(KeyCode.Z) && skillAtkCooldown < cooldowntimer)
        {

            if (!isSkillCooldown)
            {
                skillATK();
                countSkillATK++;
            }
        }

        // Auto Update the skillAtkCooldown

        // Auto detect if player use skillATK 3 times
        if (countSkillATK == 3)
        {
            countSkillATK = 0;
            isSkillCooldown = true;
            skillCooldown = 10;
        }

        cooldowntimer += Time.deltaTime;

        if (isSkillCooldown)
        {
            skillCooldown -= Time.deltaTime;

            Debug.Log(skillCooldown);
            if (skillCooldown <= 0)
            {
                isSkillCooldown = false;
            }

        }

    }
    private void FixedUpdate()
    {

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
        fireballs[FindFireball()].GetComponent<fireballATK>().SetDirection(Mathf.Sign(transform.localScale.x));
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
        skillFires[FindSkill()].GetComponent<skillFire>().setDirection(Mathf.Sign(transform.localScale.x));
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