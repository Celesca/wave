using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class PlayerSwitch : MonoBehaviour
{
    public playerMovement playerController;
    public playerMovement player2Controller;
    public bool player1Active = true;

    //Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchPlayer();
        }
    }


    public void SwitchPlayer()
    {
        if (player1Active)
        {
            playerController.enabled = false;
            player2Controller.enabled = true;
            player1Active = false;
        }
        else
        {
            playerController.enabled = true;
            player2Controller.enabled = false;
            player1Active = true;
        }
    }
}
