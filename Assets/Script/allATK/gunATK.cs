using UnityEngine;

public class gunATK : MonoBehaviour
{
    public playerMovement playerMovementScript;// script playerMovement
    public playerATK playerATK; // script playerATK

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {       
        if (Input.GetKey(KeyCode.X) && playerATK.atkCooldown < playerATK.cooldowntimer)
        {
            anim.SetTrigger("atk");
        }
    }
}
