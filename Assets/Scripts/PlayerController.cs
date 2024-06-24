using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PortalableObject
{
    private CameraMove cameraMove;
    private ScaleController scaleController;

    private void Start()
    {
        scaleController = FindObjectOfType<ScaleController>();
    }

    protected override void Awake()
    {
        base.Awake();

        cameraMove = GetComponent<CameraMove>();
    }

    public override void Warp()
    {
        base.Warp();
        scaleController.UpdateScaleFactorText();
        cameraMove.ResetTargetRotation();
    }
}
