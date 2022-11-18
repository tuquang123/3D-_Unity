using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandel : MonoBehaviour
{
    public Transform targetTransform;
    public Transform CameraTransform;
    public Transform CamerapivotTransform;
    private Transform myTransform;
    private Vector3 cameraTransformPositon;
    private LayerMask ignorelayers;
    public Vector3 cameraFollowVelocity = Vector3.zero;

    public static CameraHandel singleton;

    public float lookSpeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.03f;

    private float targetposition;
    
    float defaultPosition;
    float lookAngle;
    float pivotAngle;
    public float minimumPivot = -35;
    public float maximumPivot = 35;

    public float cameraSpheraRadius = 0.2f;
    public float cameraCollisiononOffset = 0.2f;
    public float minimumCollisionOffset = 0.2f;

    private void Awake()
    {
        singleton = this;
        myTransform = transform;
        defaultPosition = CameraTransform.localPosition.z;
        ignorelayers = ~(1 << 8 | 1 << 9 | 1 << 10);
    }

    public void Followtarget(float delta)
    {
        Vector3 targetPositon = Vector3.SmoothDamp(myTransform.position, targetTransform.position,
            ref cameraFollowVelocity, delta / followSpeed);
        myTransform.position = targetPositon;
        
        HandelCameraCollisions(delta);
    }

    public void HandleCamerarotion(float delta, float mouseXInput, float mouseYInput)
    {
        lookAngle += (mouseXInput * lookSpeed) / delta;
        pivotAngle -= (mouseYInput * pivotSpeed) / delta;
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);
        
        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        
        Quaternion targetRotation = Quaternion.Euler(rotation);
        myTransform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        targetRotation = Quaternion.Euler(rotation);
        CamerapivotTransform.localRotation = targetRotation;
    }

    void HandelCameraCollisions(float delta)
    {
        targetposition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = CameraTransform.position - CamerapivotTransform.position;
        direction.Normalize();
        if (Physics.SphereCast(CamerapivotTransform.position, cameraSpheraRadius, direction, out hit,
                MathF.Abs(targetposition), ignorelayers)){}

        float dis = Vector3.Distance(CamerapivotTransform.position, hit.point);
        targetposition = -(dis - cameraCollisiononOffset);
        if (MathF.Abs(targetposition) < minimumCollisionOffset)
        {
            targetposition = -minimumCollisionOffset;
        }

        cameraTransformPositon.z = Mathf.Lerp(CameraTransform.localPosition.z, targetposition, delta / 0.2f);
        CameraTransform.localPosition = cameraTransformPositon;

    }
}
