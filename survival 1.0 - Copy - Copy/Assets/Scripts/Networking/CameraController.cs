using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerManager player;
    public float sensitivity = 100f;
    public float clampAngle = 90f;

    private float verticalRotation;
    private float horizontalRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        verticalRotation = transform.localEulerAngles.x;
        verticalRotation = transform.localEulerAngles.y;
    }

    private void Update()
    {
        Look();
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
    }

    private void Look()
    {
        float _mouseVertical = -Input.GetAxis("Mouse Y");
        float _mouseHorizontal = Input.GetAxis("Mouse X");

        verticalRotation += _mouseVertical * sensitivity * Time.deltaTime * Convert.ToInt32(!GameUIManager.instance.paused);
        horizontalRotation += _mouseHorizontal * sensitivity * Time.deltaTime * Convert.ToInt32(!GameUIManager.instance.paused);

        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        player.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
    }
}
