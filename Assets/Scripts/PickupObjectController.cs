using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=6bFCQqabfzo

public class PickupObjectController : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform objectHoldArea;
    private GameObject heldObject;
    private Rigidbody heldObjectRB;

    [Header("Physics parameters")]
    [SerializeField] float pickupRange = 5.0f;
    [SerializeField] float pickupForce = 150.0f;

    private void Start()
    {
        UpdateParamsOnScale(1f);

    }

    private void Update()
    {
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

    public void UpdateParamsOnScale(float playerScale)
    {
        Vector3 direction = transform.forward;
        Vector3 newPos = transform.position + direction * 1.5f * playerScale;
        objectHoldArea.position = newPos;
        pickupForce = 150.0f * playerScale;
        pickupRange = 5.0f * playerScale;
    }
}
