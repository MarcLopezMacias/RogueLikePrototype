using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *   - Bonus: Crear un component que substitueix el 
 *   de moviment per un altre que dona el moviment 
 *   del personatge a l'usuari.
 *  */

public class HorizontalMovement : MonoBehaviour
{
    private float jumpSpeed;
    private float speed;
    private Rigidbody2D rb;

    private Vector2 playerInput;
    private bool shouldJump;
    private bool canJump;
    private bool canDoubleJump;

    [SerializeField]
    private int multiplyingFactor = 1000;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        speed = gameObject.GetComponent<Player>().GetSpeed();
        jumpSpeed = gameObject.GetComponent<Player>().GetJumpSpeed();
    }

    // get input values each frame
    private void Update()
    {
        // using "GetAxis" allows for many different control schemes to work here
        // go to Edit -> Project Settings -> Input to see and change them
        
        // playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        playerInput = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (canJump && Input.GetButtonDown("Jump"))
        {
            canJump = false;
            shouldJump = true;
        }
        if (canDoubleJump && Input.GetButtonDown("Jump"))
        {
            canDoubleJump = false;
            shouldJump = true;
        }

    }

    // apply physics movement based on input values
    private void FixedUpdate()
    {
        // move
        if (playerInput != Vector2.zero)
        {
            rb.AddForce(multiplyingFactor * playerInput * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        // jump
        if (shouldJump)
        {
            rb.AddForce(multiplyingFactor * Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            shouldJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // allow jumping again
        canJump = true;
        gameObject.transform.tag = "onFloor";
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        gameObject.transform.tag = "Jumping";
    }

    public bool GetMoving()
    {
        if (MovingX()) return true;
        else return false;
    }

    public bool MovingX()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0) return true;
        else return false;
    }

    public bool FacingRight()
    {
        if (Input.GetAxis("Horizontal") > 0) return true;
        else return false;
    }

    public bool FacingLeft()
    {
        if (Input.GetAxis("Horizontal")< 0) return true;
        else return false;
    }

    private void OnEnable()
    {
        Start();
    }

}

