using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationHander : MonoBehaviour
{
   public Animator anm;
   private int vectical;
   private int horital;
   public bool canRotion;

   public void Initialize()
   {
      anm = GetComponent<Animator>();
      vectical = Animator.StringToHash("Vertical");
      horital = Animator.StringToHash("Horizontal");
   }

   public void UpdateAnimatorValues(float verticalmovement, float horizontalMovement)
   {
      #region Vertical

      float v = 0;

      if (verticalmovement > 0 && verticalmovement < 0.55f)
      {
         v = 0.5f;
      }
      else if(verticalmovement > 0.55f)
      {
         v = 1;
      }
      else if(verticalmovement < 0 && verticalmovement > -0.55f)
      {
         v = -0.5f;
      }
      else if(verticalmovement <  -0.55f)
      {
         v = -1f;
      }
      else
      {
         v = 0;
      }
      
      #endregion
      
      #region Honrizontal

      float h = 0;
      if (horizontalMovement > 0 && horizontalMovement < 0.55f)
      {
         h = 0.5f;
      }
      else if (horizontalMovement > 0.55f)
      {
         h = 1;
      }
      else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
      {
         h = -0.55f;
      }
      else if (horizontalMovement < -0.55f)
      {
         h = -1;
      }
      else
      {
         h = 0;
      }
      anm.SetFloat(vectical,v,0.1f,Time.deltaTime);
      anm.SetFloat(horital,h, 0.1f, Time.deltaTime);
      #endregion
   }

   public void CanRotion()
   {
      canRotion = true;
   }
   public void StopRotion()
   {
      canRotion = false;
   }

}
