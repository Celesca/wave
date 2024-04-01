using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class dashAbility : Ability
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
        playerMovement movement = parent.GetComponent<playerMovement>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();

        rb.velocity = movement.movementInput.normalized * dashVelocity;
    }

}
