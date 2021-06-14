using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerCrouch : MonoBehaviour
{
   [Header("Crouch Details")]
   public float crouchForce;
   private bool stoppedCrouching;


   [Header("Ground Details")]
   [SerializeField] private Transform groundCheck;
   [SerializeField] private float radOCircle;
   [SerializeField] private LayerMask whatIsGround;
   public bool grounded;

   [Header("Components")]
   private Rigidbody2D rb;
   private Animator myAnimator;

    private void Start()
   {
       rb = GetComponent<Rigidbody2D>();
       myAnimator = GetComponent<Animator>();
   }
   

   private void Update()
   {
       //Basically grounded 
       grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, whatIsGround);
       
       if (grounded)
       {
           myAnimator.ResetTrigger("Crouch");
       }

       //Press S to crouch
       if (Input.GetButtonDown("Vertical") && grounded)
       {
           //Crouch!!!
           rb.velocity = new Vector2(rb.velocity.x, crouchForce);
           stoppedCrouching = false;
           //Animation
           myAnimator.SetTrigger("Crouch");
       }

       //When crouch button is inactive (released)
       if (Input.GetButtonUp("Vertical"))
       {
           stoppedCrouching = true;
           myAnimator.ResetTrigger("Crouch");
       }
   }
    private void OnDrawGizmos()
   {
       Gizmos.DrawSphere(groundCheck.position, radOCircle);
   }

   private void FixedUpdate()
   {
       HandleLayers();
   }

   private void HandleLayers()
   {
       if (!grounded)
       {
           myAnimator.SetLayerWeight(1,1);
       }
    
       else
       {
           myAnimator.SetLayerWeight(1,0);
       }

    }
}


