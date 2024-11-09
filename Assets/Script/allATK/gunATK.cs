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

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))// && (playerMovementScript.anim.GetBool("grounded") == false)
        {
            anim.SetTrigger("jump");
            anim.SetBool("isGrounded", false);
        }
        else if(playerMovementScript.anim.GetBool("grounded") == true)
        {
            anim.ResetTrigger("jump");
            anim.SetBool("isGrounded", true);
        }
    }
}
