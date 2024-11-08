using UnityEngine;

public class gunFollow : MonoBehaviour
{
    public Transform playerPoint;      // Reference to the player's transform
    public Vector3 offset;        // Offset distance between the player and the weapon
    public playerMovement playerMovementScript;

    Animator anim;

    void Awake()
    {
            playerMovementScript = playerPoint.GetComponent<playerMovement>();
            anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (playerPoint != null)
        {

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
        }
    }

    private void checkGround()
    {
        playerMovementScript.isGrounded();

    }
    private void pistonATK()
    {
        anim.SetTrigger("atk");
    }
}
