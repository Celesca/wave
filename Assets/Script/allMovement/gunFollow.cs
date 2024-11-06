using UnityEngine;

public class gunFollow : MonoBehaviour
{
    public Transform playerPoint;      // Reference to the player's transform
    public Vector3 offset;        // Offset distance between the player and the weapon
    private playerMovement playerMovementScript;

    void Start()
    {
        if (playerPoint != null)
        {
            playerMovementScript = playerPoint.GetComponent<playerMovement>();
        }
    }

    void Update()
    {
        if (playerPoint != null)
        {
            // Update the weapon's position to follow the player's position with the offset
            
            if(playerMovementScript.isCrouch == true)
            {
                transform.position = new Vector3(playerPoint.position.x, -0.502f, playerPoint.position.z);
            }
            else if(playerMovementScript.isCrouch == false)
            {
                transform.position = playerPoint.position + offset;
            }

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
}
