using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PortalableObject
{
    private CameraMove cameraMove;

    protected override void Awake()
    {
        base.Awake();

        cameraMove = GetComponentInChildren<CameraMove>();
    }

    public override void Warp()
    {
        base.Warp();
        cameraMove.moveSpeed = 5.0f * transform.localScale[0];
        cameraMove.jumpHeight = 1.5f * transform.localScale[0];
        scaleController.UpdateScaleFactorText();
        cameraMove.ResetTargetRotation();
    }
}
