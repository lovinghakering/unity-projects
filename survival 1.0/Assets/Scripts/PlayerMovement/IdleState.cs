using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public PlayerState DoState(Player player)
    {
        player.ChangeSpeed(0);
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
                return player.sprintState;
            if (Input.GetKeyDown(KeyCode.LeftControl))
                return player.crouchState;
            return player.walkState;
        }
        else
        {

            player.velocity.y += player.gravity * Time.deltaTime;

            player.controller.Move(player.velocity * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.LeftControl))
                return player.crouchState;
            return player.idleState;
        }
    }
}
