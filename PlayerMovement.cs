using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //Animation and physics
    private Rigidbody2D rb2D;
    private Animator myAnimator;

    private bool facingRight = true;

    //Variables to play with 
    public float speed = 2.0f;
    public float horizMovement; //= 1[or]-1[or]0

    // Start is called before the first frame update
    private void Start()
    {
        //Define the gameobjects found on the player
        rb2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    //Handles the input for physics
    private void Update()
    {
        //Check direction given by player
        horizMovement = Input.GetAxis("Horizontal");
    }
    
    //Handles running physics
    private void FixedUpdate()
    {
        //Move the player (left and right)
        rb2D.velocity = new Vector2(horizMovement*speed,rb2D.velocity.y);      
        Flip (horizMovement);
        myAnimator.SetFloat("Speed", Mathf.Abs(horizMovement));
    }
    //Flipping functions
    private void Flip (float horizontal)
    {
        if (horizontal < 0 && facingRight || horizontal > 0 && !facingRight)
        {
          facingRight = !facingRight;

          Vector3 theScale = transform.localScale;
          theScale.x *= -1;
          transform.localScale = theScale;
        }
    }
}
