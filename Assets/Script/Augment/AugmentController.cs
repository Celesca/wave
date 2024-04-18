using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class Augment
{
    public enum Type { Player, Weapon }
    public Type augmentType;
    public int number;

    public Augment(Type type, int num)
    {
        augmentType = type;
        number = num;
    }
}

public class AugmentController : MonoBehaviour
{
    Health health;
    playerMovement move;
    GameManagerScript gameManager;

    private Augment aug1;
    private Augment aug2;
    private Augment aug3;

    // Random Augment
    private int[] allAugmentNumbers = { 1, 2, 3, 4, 5, 6, 7, 8 };

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        move = GetComponent<playerMovement>();
        gameManager = FindObjectOfType<GameManagerScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AugmentBox"))
        {
            Debug.Log("Collided with: " + collision.gameObject.name);
            chooseAugment();
            gameManager.ChooseAugmentUI(aug1.number, aug2.number, aug3.number);
            Destroy(collision.gameObject); // Destroy Augment Box after enter
        }
    }

    private void chooseAugment()
    {
        Augment[] augments = GenerateAugment();

        aug1 = augments[0];
        aug2 = augments[1];
        aug3 = augments[2];
        Debug.Log($"Random Augments: {aug1.number}, {aug2.number}, {aug3.number}");
    }

    private Augment[] GenerateAugment()
    {
        ShuffleArray(allAugmentNumbers);

        int[] selectedNumbers = new int[3];
        Array.Copy(allAugmentNumbers, selectedNumbers, 3);

        Augment[] augments = new Augment[3];

        for (int i = 0; i < 3; i++)
        {
            int augmentNumber = allAugmentNumbers[i];
            Augment.Type augmentType = (augmentNumber <= 4) ? Augment.Type.Player : Augment.Type.Weapon;
            augments[i] = new Augment(augmentType, augmentNumber);
        }

        return augments;
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

    private void PerformAction(Augment augment)
    {
        switch (augment.augmentType)
        {
            case Augment.Type.Player:
                switch (augment.number)
                {
                    case 1:
                        Debug.Log("Health Boost");
                        healthBoost();
                        break;
                    case 2:
                        Debug.Log("Armor Boost");
                        armorBoost();
                        break;
                    case 3:
                        Debug.Log("Double Jump");
                        doubleJump();
                        break;
                    case 4:
                        Debug.Log("Anti-Projectile");
                        //
                        break;
                }
                break;
            case Augment.Type.Weapon:
                switch (augment.number)
                {
                    case 5:
                        Debug.Log("Cold Water Gun");
                        //
                        break;
                    case 6:
                        Debug.Log("Burning Water Gun");
                        //
                        break;
                    case 7:
                        Debug.Log("Toxic Water Gun");
                        //
                        break;
                    case 8:
                        Debug.Log("Cooldown Reset");
                        //
                        break;
                }
                break;
        }
    }

    private void healthBoost()
    {
        health.healthBoost();
    }

    private void armorBoost()
    {
        health.armorBoost();
    }

    private void doubleJump()
    {
        move.enableDoubleJump();
    }
}
