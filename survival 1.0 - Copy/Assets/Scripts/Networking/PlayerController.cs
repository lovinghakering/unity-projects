using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Keys keys;

    private void Awake()
    {
        keys = GameManager.instance.keys;
    }

    private void FixedUpdate()
    {
        SendInput();
    }

    private void SendInput()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(keys.MoveForward),
            Input.GetKey(keys.MoveBackward),
            Input.GetKey(keys.MoveLeft),
            Input.GetKey(keys.MoveRight),
            Input.GetKey(keys.Jump)
        };

        ClientSend.PlayerMovement(_inputs);
    }
}
