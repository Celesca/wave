using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    public float cooldownTime = 5f;
    public float activeTime = 1f;

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    //Initialize State
    AbilityState state = AbilityState.ready;

    public KeyCode key = KeyCode.LeftShift;

    private Rigidbody2D rb;

    // Player Object Injection
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogError("No Rigidbody2D found on player object");
    }

    // Dash Forward
    public void Activate()
    {
        if (rb == null)
            Debug.LogError("No Rigidbody2D found on player object");

        // Determine the direction based on the player's local scale
        float dashDirection = Mathf.Sign(transform.localScale.x);

        Debug.Log("Dash Direction: " + dashDirection);
        // Implement the logic for the dash ability activation here
        // Move the player forward quickly in the determined direction.
        rb.velocity = new Vector2(dashDirection * 10f, 0f);
    }

    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:

                if(Input.GetKeyDown(key)) {
                    Debug.Log("Tested");

                    Activate();
                    state = AbilityState.active;
                    activeTime = ability.activeTime;
                }
                break;


            case AbilityState.active:
                {
                    if (activeTime > 0)
                    {
                        activeTime -= Time.deltaTime;
                    }
                    else
                    {
                        state = AbilityState.cooldown;
                        cooldownTime = ability.cooldownTime;
                    }
                    break;
                }
            case AbilityState.cooldown:
                {
                    if(cooldownTime > 0)
                    {
                        cooldownTime -= Time.deltaTime;
                    }
                    else
                    {
                        state = AbilityState.ready;
                    }
                    break;
                }
                
        }
    }
}
