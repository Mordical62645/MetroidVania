using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{  
   Rigidbody2D rb;
   Animator animator;
   [SerializeField] Collider2D StandingCollider;
   [SerializeField] Transform GroundCheckCollider;
   [SerializeField] LayerMask GroundLayer;

   const float GroundCheckRadius = 0.2f;
   float horizontalValue;
   
   bool IsGrounded;
   [SerializeField]  bool crouch;

    void Awake()
   {
       rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
   }
   
   void Update()
   {
      if (Input.GetButtonDown("Crouch"))
      {
          crouch = true;
      }
      else if (Input.GetButtonUp("Crouch"))
      {
          crouch = false;
      }
   }

   void FixedUpdate()
   {
      GroundCheck();
      Move(horizontalValue, crouch);
   }
   
   void GroundCheck()
   {
       IsGrounded = false;
       Collider2D[] Colliders = Physics2D.OverlapCircleAll(GroundCheckCollider.position, GroundCheckRadius, GroundLayer);
       if (Colliders.Length > 0)
       {
           IsGrounded = true;
       }
   }

   void Move(float dir, bool crouchFlag)
   {
      #region Crouch
      if (IsGrounded)
      {
         StandingCollider.enabled = !crouchFlag;
      }

      animator.SetBool("Crouch", crouchFlag);
      
      #endregion
   }
}

