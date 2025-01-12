using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapGun : MonoBehaviour
{
    [SerializeField] private AudioSource swapWeaponSoundEffect;

    //For swap weapon
    public int currentSwap;

    [SerializeField] private Animator anim;
    [SerializeField] private playerMovement playerMovement;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            swapWeaponSoundEffect.Play();
            swapWeapon();
        }
    }
    //swap weapon
    void swapWeapon()
    {
        if (currentSwap == 0)
        {
            currentSwap += 1;
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
        }
        else if (currentSwap == 1)
        {
            currentSwap += 1;
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(2, 1);
            anim.SetLayerWeight(3, 0);
        }
        else if (currentSwap == 2)
        {
            currentSwap += 1;
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(3, 1);
        }
        else if (currentSwap == 3)
        {
            currentSwap -= 3;
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(0, 0);
        }
    }
}
