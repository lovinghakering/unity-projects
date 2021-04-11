using UnityEngine;

public class CrouchState : PlayerState
{
    public PlayerState DoState(Player player)
    {
        player.ChangeSpeed(player.walkSpeed);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Vector3 move = player.transform.right * x + player.transform.forward * z;
            player.controller.Move(move * player.currentSpeed * Time.deltaTime);
            player.velocity.y += player.gravity * Time.deltaTime;
            player.controller.Move(player.velocity * Time.deltaTime);
            return player.crouchState;
        }

        if (x != 0 || z != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                return player.sprintState;
            else
                return player.walkState;
        }
        else
        {
            return player.idleState;
        }
    }
}
