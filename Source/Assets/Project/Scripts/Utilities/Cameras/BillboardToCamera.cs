using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component makes an object forward vector always point away from the main camera,
/// but parallel to the ground.
/// </summary>
public class BillboardToCamera : MonoBehaviour
{
    private Camera mainCam;

    public bool onlyUseCameraForward = false;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 dirToPoint;

        if (!onlyUseCameraForward)
        {
            dirToPoint = Vector3.ProjectOnPlane(transform.position - mainCam.transform.position, Vector3.up);
        }
        else
        {
            dirToPoint = Vector3.ProjectOnPlane(mainCam.transform.forward, Vector3.up);
        }

        transform.forward = dirToPoint;
    }
}
