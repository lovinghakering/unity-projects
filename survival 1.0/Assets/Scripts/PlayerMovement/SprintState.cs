using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintState : PlayerState
{
    public PlayerState DoState(Player player)
    {
        player.ChangeSpeed(player.sprintSpeed);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            return player.crouchState;
        }

        if (x != 0 || z != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 move = player.transform.right * x + player.transform.forward * z;
                player.controller.Move(move * player.currentSpeed * Time.deltaTime);
                player.velocity.y += player.gravity * Time.deltaTime;
                player.controller.Move(player.velocity * Time.deltaTime);
                return player.sprintState;
            }
            return player.walkState;
        }
        else
        {
            return player.idleState;
        }
    }
}
