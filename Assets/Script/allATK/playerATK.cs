using UnityEngine;

public class playerATK : MonoBehaviour
{
    [SerializeField] private float atkCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [SerializeField] private float skillAtkCooldown;
    [SerializeField] private Transform skillPoint;
    [SerializeField] private GameObject[] skillFires;

    private Animator anim;
    private playerMovement PlayerMovement;
    private float skillCooldown;

    private float cooldowntimer = Mathf.Infinity;

    private bool isSkillCooldown = false;
    private static int countSkillATK = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        PlayerMovement = GetComponent<playerMovement>();
    }
    private void Update()
    {
        if ((Input.GetKey(KeyCode.X) || Input.GetMouseButton(0)) && atkCooldown < cooldowntimer)
            Atk();

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
        for(int i = 0; i<fireballs.Length; i++)
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
        countSkillATK = 0;
        
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

