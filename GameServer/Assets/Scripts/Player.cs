using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private bool isGrounded;

    private Inputs inputs;
    private Vector3 velocity;

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
    }

    public void FixedUpdate()
    {
        Vector2 _inputDirection = Vector2.zero;
        if (inputs.MoveForward)
        {
            _inputDirection.y += 1;
        }
        if (inputs.MoveBackward)
        {
            _inputDirection.y -= 1;
        }
        if (inputs.MoveLeft)
        {
            _inputDirection.x -= 1;
        }
        if (inputs.MoveRight)
        {
            _inputDirection.x += 1;
        }

        Move(_inputDirection);
    }

    private void Move(Vector2 _inputDirection)
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;

        controller.Move(move * moveSpeed * Time.deltaTime);

        if (inputs.Jump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
    }

    public void SetInput(Inputs _inputs, Quaternion _rotation)
    {
        inputs = _inputs;
        transform.rotation = _rotation;
    }
}
