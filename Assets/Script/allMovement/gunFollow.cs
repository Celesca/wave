using UnityEditor;
using System.Collections;
using UnityEngine;

public class gunFollow : MonoBehaviour
{
    public Transform playerPoint;      // Reference to the player's transform
    public Vector3 offset;        // Offset distance between the player and the weapon
    public playerMovement playerMovementScript;

    public playerATK playerATK; // script playerATK

    private bool isGrounded = true;

    [SerializeField] private Health health;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private float iFramesDuration;
    private SpriteRenderer spriteRend;
    Animator anim;

    void Awake()
    {
        playerMovementScript = playerPoint.GetComponent<playerMovement>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        if (health.armor > 0)
        {
            spriteRend.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }

    void Update()
    {

        if (playerPoint != null)
        {
            isGrounded = playerMovementScript.grounded;
            /** Flip Anim **/
            // Get the player's facing direction and flip the weapon accordingly
            Vector3 facingDirection = playerMovementScript.GetFacingDirection();
            Debug.Log("Facing Direction: " + facingDirection);

            // Ensure the gun flips correctly based on the player's facing direction
            if (facingDirection.x != 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                Debug.Log("Gun Scale: " + transform.localScale);
            }
            else
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }


            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
            {
                anim.SetTrigger("jump");
                isGrounded = false;
            }
            else
            {
                anim.ResetTrigger("jump");
                isGrounded = true;

            }
            anim.SetBool("isGrounded", isGrounded);
        }
    }

    private void checkGround()
    {
        playerMovementScript.isGrounded();
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
}
