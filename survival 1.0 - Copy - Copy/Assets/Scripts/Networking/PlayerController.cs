using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Keys keys;
    public bool allowMovement = true;

    private void FixedUpdate()
    {
        SendInput();
    }

    private void SendInput()
    {
        if (allowMovement)
        {
            Inputs input = new Inputs
            (
                Input.GetKey(keys.MoveForward),
                Input.GetKey(keys.MoveBackward),
                Input.GetKey(keys.MoveLeft),
                Input.GetKey(keys.MoveRight),
                Input.GetKey(keys.Jump)
            );

            ClientSend.PlayerMovement(input);
        }
        else
        {
            Inputs input = new Inputs
            (
                false,
                false,
                false,
                false,
                false
            );

            ClientSend.PlayerMovement(input);
        }
    }
}
