﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshFilter))]
//[RequireComponent(typeof(MeshRenderer))]
//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(Collider))]
public class PortalableObject : MonoBehaviour
{
    private GameObject cloneObject;

    private int inPortalCount = 0;
    
    private Portal inPortal;
    private Portal outPortal;

    private new Rigidbody rigidbody;
    protected new Collider collider;

    

    private static readonly Quaternion halfTurn = Quaternion.Euler(0.0f, 180.0f, 0.0f);

    //Set up for scaling
    public ScaleController scaleController;
    public float currentScale = 0;
    private void Start()
    {
        scaleController = FindObjectOfType<ScaleController>();
    }


    protected virtual void Awake()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            return;
        }
        cloneObject = new GameObject();
        cloneObject.SetActive(false);
        var meshFilter = cloneObject.AddComponent<MeshFilter>();
        var meshRenderer = cloneObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = GetComponentInParent<MeshFilter>().mesh;
        meshRenderer.materials = GetComponentInParent<MeshRenderer>().materials;
        cloneObject.transform.localScale = transform.localScale;

        rigidbody = GetComponentInParent<Rigidbody>();
        collider = GetComponentInParent<Collider>();
    }

    private void LateUpdate()
    {
        if(inPortal == null || outPortal == null)
        {
            return;
        }

        if(cloneObject.activeSelf && inPortal.IsPlaced() && outPortal.IsPlaced())
        {
            var inTransform = inPortal.transform;
            var outTransform = outPortal.transform;

            // Update position of clone.
            Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
            relativePos = halfTurn * relativePos;
            cloneObject.transform.position = outTransform.TransformPoint(relativePos);

            // Update rotation of clone.
            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
            relativeRot = halfTurn * relativeRot;
            cloneObject.transform.rotation = outTransform.rotation * relativeRot;

            //update scale of clone
            float scaleRatio = outPortal.currentScale / inPortal.currentScale;
            cloneObject.transform.localScale *= scaleRatio;
        }
        else
        {
            cloneObject.transform.position = new Vector3(-1000.0f, 1000.0f, -1000.0f);
        }
    }

    public void SetIsInPortal(Portal inPortal, Portal outPortal, Collider wallCollider)
    {
        this.inPortal = inPortal;
        this.outPortal = outPortal;

        Physics.IgnoreCollision(collider, wallCollider);

        cloneObject.SetActive(true);

        ++inPortalCount;
    }

    public virtual void Warp()
    {
        var inTransform = inPortal.transform;
        var outTransform = outPortal.transform;

        // Update position of object.
        Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
        relativePos = halfTurn * relativePos;
        transform.position = outTransform.TransformPoint(relativePos);

        // Update rotation of object.
        Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
        relativeRot = halfTurn * relativeRot;
        transform.rotation = outTransform.rotation * relativeRot;

        // Update velocity of rigidbody.
        Vector3 relativeVel = inTransform.InverseTransformDirection(rigidbody.velocity);
        relativeVel = halfTurn * relativeVel;
        rigidbody.velocity = outTransform.TransformDirection(relativeVel);

        //update Scale of the object
        float scaleRatio = outPortal.currentScale / inPortal.currentScale;
        transform.localScale *= scaleRatio;

        // Swap portal references.
        var tmp = inPortal;
        inPortal = outPortal;
        outPortal = tmp;
    }


    public void ExitPortal(Collider wallCollider)
    {
        Physics.IgnoreCollision(collider, wallCollider, false);
        --inPortalCount;

        if (inPortalCount == 0)
        {
            cloneObject.SetActive(false);
        }
    }
}
