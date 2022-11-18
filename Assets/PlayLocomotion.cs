using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLocomotion : MonoBehaviour
{
   private Transform cameraObject;
   private InputHander inputHander;
   private Vector3 moveDirection;

   [HideInInspector] public Transform MyTransform;
   [HideInInspector] public AnimationHander AnimationHander;
   public new Rigidbody rigidbody;
   public GameObject normalCamera;

   [Header("starts")] [SerializeField] private float movementSpeed = 5;
   [SerializeField] private float rotationSpeed = 10f;

   private void Start()
   {
      rigidbody = GetComponent<Rigidbody>();
      inputHander = GetComponent<InputHander>();
      AnimationHander = GetComponentInChildren<AnimationHander>();
      cameraObject = Camera.main.transform;
      MyTransform = transform;
      AnimationHander.Initialize();
   }

   private void Update()
   {
      float delta = Time.deltaTime;
      inputHander.TickInput(delta);

      moveDirection = cameraObject.forward * inputHander.vertical;
      moveDirection += cameraObject.right * inputHander.horizontal;
      moveDirection.Normalize();
      moveDirection.y = 0;

      float speed = movementSpeed;
      moveDirection *= speed;

      Vector3 protecteadVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
      rigidbody.velocity = protecteadVelocity;
      
      AnimationHander.UpdateAnimatorValues(inputHander.moveAmount,0);

      if (AnimationHander.canRotion)
      {
         HandleRotation(delta);
      }
   }

   #region Move

   private Vector3 normalVector;
   private Vector3 targetPositon;

   private void HandleRotation(float delta)
   {
      Vector3 targetDir = Vector3.zero;
      float moveOverride = inputHander.moveAmount;

      targetDir = cameraObject.forward * inputHander.vertical;
      targetDir += cameraObject.right * inputHander.horizontal;
      
      targetDir.Normalize();
      targetDir.y = 0;

      if (targetDir == Vector3.zero)
      {
         targetDir = MyTransform.forward;
      }

      float rs = rotationSpeed;
      
      Quaternion tr = Quaternion.LookRotation((targetDir));
      Quaternion targetRotion = Quaternion.Slerp(MyTransform.rotation,tr,rs *delta);

      MyTransform.rotation = targetRotion;

   }


   #endregion
}
