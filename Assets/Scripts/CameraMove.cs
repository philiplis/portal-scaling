//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
//public class CameraMove : MonoBehaviour
//{
//    private const float moveSpeed = 7.5f;
//    private const float cameraSpeed = 3.0f;

//    public Quaternion TargetRotation { private set; get; }

//    private Vector3 moveVector = Vector3.zero;
//    private float moveY = 0.0f;

//    private new Rigidbody rigidbody;

//    private void Awake()
//    {
//        rigidbody = GetComponent<Rigidbody>();
//        Cursor.lockState = CursorLockMode.Locked;

//        TargetRotation = transform.rotation;
//    }

//    private void Update()
//    {
//        // Rotate the camera.
//        var rotation = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
//        var targetEuler = TargetRotation.eulerAngles + (Vector3)rotation * cameraSpeed;
//        if(targetEuler.x > 180.0f)
//        {
//            targetEuler.x -= 360.0f;
//        }
//        targetEuler.x = Mathf.Clamp(targetEuler.x, -75.0f, 75.0f);
//        TargetRotation = Quaternion.Euler(targetEuler);

//        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, 
//            Time.deltaTime * 15.0f);

//        // Move the camera.
//        float x = Input.GetAxis("Horizontal");
//        float z = Input.GetAxis("Vertical");
//        moveVector = new Vector3(x, 0.0f, z) * moveSpeed;

//        moveY = Input.GetAxis("Elevation");
//    }

//    private void FixedUpdate()
//    {
//        Vector3 newVelocity = transform.TransformDirection(moveVector);
//        newVelocity.y += moveY * moveSpeed;
//        rigidbody.velocity = newVelocity;
//    }

//    public void ResetTargetRotation()
//    {
//        TargetRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
//    }
//}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
//public class CameraMove : MonoBehaviour
//{
//    private const float moveSpeed = 7.5f;
//    private const float cameraSpeed = 3.0f;

//    public float gravity = 9.8f;
//    public float jumpHeight = 2.0f;

//    public Quaternion TargetRotation { private set; get; }

//    private Vector3 moveVector = Vector3.zero;
//    private float moveY = 0.0f;

//    private new Rigidbody rigidbody;

//    private void Awake()
//    {
//        rigidbody = GetComponent<Rigidbody>();
//        Cursor.lockState = CursorLockMode.Locked;

//        TargetRotation = transform.rotation;
//    }

//    private void Update()
//    {
//        // Rotate the camera.
//        var rotation = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
//        var targetEuler = TargetRotation.eulerAngles + (Vector3)rotation * cameraSpeed;
//        if (targetEuler.x > 180.0f)
//        {
//            targetEuler.x -= 360.0f;
//        }
//        targetEuler.x = Mathf.Clamp(targetEuler.x, -75.0f, 75.0f);
//        TargetRotation = Quaternion.Euler(targetEuler);

//        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation,
//            Time.deltaTime * 15.0f);

//        // Move the camera.
//        float x = Input.GetAxis("Horizontal");
//        float z = Input.GetAxis("Vertical");
//        moveVector = new Vector3(x, 0.0f, z) * moveSpeed;

//        moveY = Input.GetAxis("Elevation");
//    }

//    private void FixedUpdate()
//    {
//        Vector3 newVelocity = transform.TransformDirection(moveVector);
//        newVelocity.y += moveY * moveSpeed;
//        rigidbody.velocity = newVelocity;
//    }

//    public void ResetTargetRotation()
//    {
//        TargetRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
//    }
//}
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
    public float gravity = 9.8f;
    public float jumpHeight = 1.5f;

    public UnityEngine.Quaternion TargetRotation { get; private set; }

    Rigidbody rb;
    private UnityEngine.Vector3 moveVector = UnityEngine.Vector3.zero;


    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;

    private void Awake()
    {
        Time.timeScale = 1;
        rb = GetComponentInParent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        TargetRotation = transform.rotation;
    }

    private void Update()
    {


        // Move the camera.
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
        //moveVector = new Vector3(x, 0.0f, z) * moveSpeed;
        if (!PauseController.gameIsPaused)
        {
            PlayerInput();
        }

        // Jumping logic
        
        //if (IsGrounded())
        //{
        //    verticalVelocity = -gravity * Time.deltaTime;

        //    if (Input.GetButtonDown("Jump"))
        //    {
        //        verticalVelocity = Mathf.Sqrt(jumpHeight * 2 * gravity);
        //    }
        //}
        //else
        //{
        //    verticalVelocity -= gravity * Time.deltaTime;
        //}
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
        targetEuler.x = Mathf.Clamp(targetEuler.x, -75.0f, 75.0f);
        TargetRotation = UnityEngine.Quaternion.Euler(targetEuler);

        transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation, TargetRotation,
            Time.deltaTime * 15.0f);

        //Player Movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

        //Player jump
        if ( Input.GetButtonDown("Jump") && IsGrounded())
        {
            //force.y = 100 * transform.parent.transform.localScale[0]
            rb.AddForce(new Vector3(0, 20 * jumpHeight, 0), ForceMode.Impulse);

        }
        
    }
    private void FixedUpdate()
    {
        Vector3 force = moveDirection.normalized * moveSpeed * 10f;
        force.y = -9.8f;
        rb.AddForce(force, ForceMode.Acceleration);
    }
    //private void FixedUpdate()
    //{
        
    //    //moveVector[1] 
    //    UnityEngine.Vector3 newVelocity = transform.TransformDirection(moveVector);
    //    //newVelocity.y = verticalVelocity;

    //    //characterController.Move(newVelocity * Time.deltaTime);
    //    rb.velocity = newVelocity;
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        rb.AddForce(new Vector3(0, 15 * jumpHeight, 0), ForceMode.Impulse);
    //    }
    //}

    public void ResetTargetRotation()
    {
        TargetRotation = UnityEngine.Quaternion.LookRotation(transform.forward, UnityEngine.Vector3.up);
    }

    bool IsGrounded()
    {
        return (GetComponentInParent<Rigidbody>().velocity.y == 0) ;
    }
}