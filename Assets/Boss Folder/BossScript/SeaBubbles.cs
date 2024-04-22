using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaBubbles : MonoBehaviour
{
    public GameObject seaBubbles;
    public Transform seaBuubblesPos;


    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
    }
    //normalATK stop atk 
    void Shoot()
    {
        Instantiate(seaBubbles, seaBuubblesPos.position, Quaternion.identity);
    }

}
