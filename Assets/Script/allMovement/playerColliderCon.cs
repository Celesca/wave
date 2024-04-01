using UnityEngine;

public class playerColliderCon : MonoBehaviour
{
    public playerMovement thePlayer;
    public BoxCollider2D playerCol;

    [SerializeField] Vector2 Idleoffset, Idlesize;
    [SerializeField] Vector2 crouchoffset, crouchsize;

    private void Start()
    {
        thePlayer = GetComponent<playerMovement>();

        Idlesize = playerCol.size;
        Idleoffset = playerCol.offset;
    }
    //set collider size if crouch
    private void Update()
    {
        if (thePlayer.isCrouch)
        {
            playerCol.size = crouchsize;
            playerCol.offset = crouchoffset;
        }
        else
        {
            playerCol.size = Idlesize;
            playerCol.offset = Idleoffset;    
        }
    }
}
