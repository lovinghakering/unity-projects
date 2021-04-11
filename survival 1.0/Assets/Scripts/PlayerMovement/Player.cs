using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("State")]
    public string currentStateName;
    private PlayerState currentState;

    public IdleState idleState = new IdleState();
    public WalkState walkState = new WalkState();
    public SprintState sprintState = new SprintState();
    public CrouchState crouchState = new CrouchState();


    [Header("Camera")]
    public Camera fpsCamera;
    public float mouseSensitivity = 200f;
    float xRotation;


    [Header("Controller")]
    public CharacterController controller;
    public Transform groundCheck;
    public bool isGrounded;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float walkFOV;
    public float sprintFOV;
    public float currentSpeed;
    public float walkSpeed = 9f;
    public float sprintSpeed = 13f;
    public float jumpHeight = 3;
    public float gravity = -9.81f;
    public Vector3 velocity;


    private void OnEnable()
    {
        currentState = idleState;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        DebugMenu.instance.UpdateStatistics(this.transform.position, currentState, currentSpeed, fpsCamera.fieldOfView, isGrounded, velocity);
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        currentState = currentState.DoState(this);
        currentStateName = currentState.ToString();

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        //Camera controll
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        fpsCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void ChangeSpeed(float speed)
    {
        if (currentState != crouchState)
        {
            currentSpeed = speed;
        }
        else
        {
            currentSpeed = 4;
        }
    }
}
