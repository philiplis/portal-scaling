using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PortalableObject
{
    private CameraMove cameraMove;
    private PickupObjectController pickupObjectController;

    protected override void Awake()
    {
        base.Awake();

        cameraMove = GetComponentInChildren<CameraMove>();
        pickupObjectController = GetComponentInChildren<PickupObjectController>();
    }

    public override void Warp()
    {
        
        base.Warp();
        float newPlayerScale = transform.localScale[0];
        pickupObjectController.UpdateParamsOnScale(newPlayerScale);
        cameraMove.moveSpeed = newPlayerScale*5.0f;
        cameraMove.jumpHeight = 1.5f * newPlayerScale; //*1.5f
        scaleController.UpdateScaleFactorText();
        cameraMove.ResetTargetRotation();
        //Quaternion currentRotation = transform.rotation;
        //Quaternion newRotation = Quaternion.Euler(0, currentRotation.eulerAngles.y, 0);
        //transform.rotation = newRotation;
    }
}
