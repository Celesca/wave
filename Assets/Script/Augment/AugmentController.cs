using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class AugmentController : MonoBehaviour
{
    Health health;
    playerMovement move;
    GameManagerScript gameManager;

    private int aug1;
    private int aug2;
    private int aug3;

    // Random Augment
    private int[] allAugmentNumbers = { 1, 2, 3, 4, 5, 6, 7, 8 };

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        move = GetComponent<playerMovement>();
        gameManager = FindObjectOfType<GameManagerScript>();
    }

    // collision with augment box
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AugmentBox"))
        {
            Debug.Log("Collided with: " + collision.gameObject.name);
            getAugmentIndex();
            gameManager.GetSelectedAugment(aug1, aug2, aug3);
            Destroy(collision.gameObject);
        }
    }

    // set value aug1, aug2, aug3
    private void getAugmentIndex()
    {
        int[] augmentNumbers = GenerateAugmentNumbers();

        aug1 = augmentNumbers[0];
        aug2 = augmentNumbers[1];
        aug3 = augmentNumbers[2];
        Debug.Log($"Random Augments: {aug1}, {aug2}, {aug3}");
    }

    // send 3 value of first 3 in augment array
    private int[] GenerateAugmentNumbers()
    {
        ShuffleArray(allAugmentNumbers);

        int[] selectedNumbers = new int[3];
        Array.Copy(allAugmentNumbers, selectedNumbers, 3);

        return selectedNumbers;
    }

    private void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    // delete augment number that has been selected by player
    private void DeleteSelectedAugment(int augment)
    {
        int index = Array.IndexOf(allAugmentNumbers, augment);

        if (index == -1)
        {
            Debug.Log($"Number {augment} not found in the array.");
            return;
        }

        for (int i = index; i < allAugmentNumbers.Length - 1; i++)
        {
            allAugmentNumbers[i] = allAugmentNumbers[i + 1];
        }

        Array.Resize(ref allAugmentNumbers, allAugmentNumbers.Length - 1);
    }

    // 
    public void PerformAction(int augment)
    {
        switch (augment)
        {
            case 1:
                healthBoost();
                break;
            case 2:
                armorBoost();
                break;
            case 3:
                doubleJump();
                break;
            case 4:
                antiProjectile();
                break;
            case 5:
                coldWaterGun();
                break;
            case 6:
                burningWaterGun();
                break;
            case 7:
                toxicWaterGun();
                break;
            case 8:
                cooldownReset();
                break;
            default:
                Debug.Log("No Augment Selected");
                break;
        }

        Debug.Log("Augment selected performed: " + augment);
        DeleteSelectedAugment(augment);
    }

    /* call to other classes to perform augment */

    private void healthBoost()
    {
        Debug.Log("Health Boost");
        health.healthBoost();
    }

    private void armorBoost()
    {
        Debug.Log("Armor Boost");
        health.armorBoost();
    }

    private void doubleJump()
    {
        Debug.Log("Double Jump");
        move.enableDoubleJump();
    }

    private void antiProjectile()
    {
        Debug.Log("Anti-Projectile");
        //
    }

    private void coldWaterGun()
    {
        Debug.Log("Cold Water Gun");
        //
    }

    private void burningWaterGun()
    {
        Debug.Log("Burning Water Gun");
        //
    }

    private void toxicWaterGun()
    {
        Debug.Log("Toxic Water Gun");
        //
    }

    private void cooldownReset()
    {
        Debug.Log("Cooldown Reset");
        //
    }
}
