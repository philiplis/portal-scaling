using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//[RequireComponent(typeof(Rigidbody))]
public class CameraMove : MonoBehaviour
{
    public float cameraSpeed = 2.0f;
    public float moveSpeed = 5.0f;
    public float jumpHeight = 1.5f;

    public UnityEngine.Quaternion TargetRotation { get; private set; }

    Rigidbody rb;
    private UnityEngine.Vector3 moveVector = UnityEngine.Vector3.zero;

    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;
    ScaleController sc;

    private void Awake()
    {
        Time.timeScale = 1;
        rb = GetComponentInParent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        TargetRotation = transform.rotation;
        sc = FindObjectOfType<ScaleController>();

    }

    private void Update()
    {
        if (!PauseController.gameIsPaused)
        {
            PlayerInput();
        }
    }

    void PlayerInput()
    {
        // Rotate the camera.
        var rotation = new UnityEngine.Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        var targetEuler = TargetRotation.eulerAngles + (UnityEngine.Vector3)rotation * cameraSpeed;
        if (targetEuler.x > 180.0f)
        {
            targetEuler.x -= 360.0f;
        }
        targetEuler.x = Mathf.Clamp(targetEuler.x, -90.0f, 89.0f);
        TargetRotation = UnityEngine.Quaternion.Euler(targetEuler);

        transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation, TargetRotation,
            Time.deltaTime * 15.0f);

        // Player Movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

        // Player jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(new Vector3(0, Mathf.Sqrt(2 * jumpHeight * Physics.gravity.magnitude), 0), ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        Vector3 horizontalForce = moveDirection.normalized * moveSpeed;
        Vector3 totalForce = new Vector3(horizontalForce.x, rb.velocity.y, horizontalForce.z);

        rb.velocity = totalForce;
    }

    public void ResetTargetRotation()
    {
        TargetRotation = UnityEngine.Quaternion.LookRotation(transform.forward, UnityEngine.Vector3.up);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.5f * sc.GetPlayerScale() ); //TODO Multiply by Scale factor
    }
}
