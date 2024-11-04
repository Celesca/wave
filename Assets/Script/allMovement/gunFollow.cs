using UnityEngine;

public class gunFollow : MonoBehaviour
{
    public Transform player;      // Reference to the player's transform
    public Vector3 offset;        // Offset distance between the player and the weapon
    private playerMovement playerMovementScript;

    void Start()
    {
        if (player != null)
        {
            playerMovementScript = player.GetComponent<playerMovement>();
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Update the weapon's position to follow the player's position with the offset
            transform.position = player.position + offset;

            // Get the player's facing direction and flip the weapon accordingly
            Vector3 facingDirection = playerMovementScript.GetFacingDirection();
            transform.localScale = new Vector3(facingDirection.x * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
