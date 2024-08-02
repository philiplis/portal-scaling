using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=6bFCQqabfzo

public class PickupObjectController : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform objectHoldArea;
    [SerializeField] float holdAreaDistanceMultiplier = 2.0f;
    private GameObject heldObject;
    private Rigidbody heldObjectRB;

    [Header("Physics parameters")]
    [SerializeField] float pickupRange = 5.0f;
    [SerializeField] float pickupForce = 150.0f;
    [SerializeField] float throwForce = 5.0f;

    float playerScale = 1f;

    private void Start()
    {
        UpdateParamsOnScale(1f);

    }

    private void Update()
    {
        UpdatePickupAreaLocation();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(heldObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }
        if (heldObject != null)
        {
            MoveObject();
            if (Input.GetKeyDown(KeyCode.R))
            {
                ThrowObject();
            }
        }
    }

    void PickupObject(GameObject pickObject)
    {
        if (pickObject.GetComponent<Rigidbody>())
        {
            heldObjectRB = pickObject.GetComponent<Rigidbody>();
            heldObjectRB.useGravity = false;
            heldObjectRB.drag = 10;
            heldObjectRB.constraints = RigidbodyConstraints.FreezeRotation;
            heldObjectRB.transform.parent = objectHoldArea;
            heldObject = pickObject;
        }
    }

    void DropObject()
    {
        heldObjectRB.useGravity = true;
        heldObjectRB.drag = 1;
        heldObjectRB.constraints = RigidbodyConstraints.None;
        heldObject.transform.parent = null;
        heldObject = null;
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObject.transform.position, objectHoldArea.position) > 0.1f)
        {
            Vector3 moveDir = (objectHoldArea.position - heldObject.transform.position);
            heldObjectRB.AddForce(moveDir * pickupForce);
        }
    }

    void ThrowObject()
    {
        DropObject();
        heldObjectRB.AddForce( (transform.forward * playerScale * throwForce), ForceMode.Impulse);
        //TODO incorporate player's current velocity to the force above 
    }

    void UpdatePickupAreaLocation()
    {
  
        Vector3 direction = transform.forward;
        Vector3 newPos = transform.position + (direction * holdAreaDistanceMultiplier * playerScale);
        objectHoldArea.position = newPos;
    }

    public void UpdateParamsOnScale(float newPlayerScale)
    {
        playerScale = newPlayerScale;
        UpdatePickupAreaLocation();
        pickupForce = 150.0f * playerScale;
        pickupRange = 5.0f * playerScale;
    }
}
