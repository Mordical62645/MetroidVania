using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
   //Force, apply force, 1x
   //rb.velocity = new Vector2(rb.velocity.x, jumpforce);

   [Header("Jump Details")]
   public float jumpForce;
   public float jumpTime;
   private float jumpTimeCounter;
   private bool stoppedJumping;

   [Header("Ground Details")]
   [SerializeField] private Transform groundCheck;
   [SerializeField] private float radOCircle;
   [SerializeField] private LayerMask whatIsGround;
   public bool grounded;

   [Header("Components")]
   private Rigidbody2D rb;
   
   
   private void Start()
   {
       rb = GetComponent<Rigidbody2D>();
       jumpTimeCounter = jumpTime;
   }

   private void Update()
   {
       //Basically grounded 
       grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, whatIsGround);
       
       if (grounded)
       {
           jumpTimeCounter = jumpTime;
       }

       //Use w or space to jump
       if(Input.GetButtonDown("Jump") && grounded)
       {
           //Jump!!!
           rb.velocity = new Vector2(rb.velocity.x, jumpForce);
           stoppedJumping = false;
       }

       //To keep jumping while button is active
       if(Input.GetButton("Jump") && !stoppedJumping && (jumpTimeCounter > 0))
       {
           //Jump!!
           rb.velocity = new Vector2(rb.velocity.x, jumpForce);
           jumpTimeCounter -= Time.deltaTime;
       }

       //When jump button is inactive (released)
       if (Input.GetButtonUp("Jump"))
       {
           jumpTimeCounter = 0;
           stoppedJumping = true;
       }
   }
   private void OnDrawGizmos()
   {
       Gizmos.DrawSphere(groundCheck.position, radOCircle);
   }
}
