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
    private float speed;
    private Rigidbody2D rb;

    private Vector2 playerInput;

    [SerializeField]
    private int multiplyingFactor = 1000;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        speed = gameObject.GetComponent<Player>().GetSpeed();
    }

    // get input values each frame
    private void Update()
    {
        // using "GetAxis" allows for many different control schemes to work here
        // go to Edit -> Project Settings -> Input to see and change them
        
        // playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        playerInput = new Vector2(Input.GetAxis("Horizontal"), 0);

    }

    // apply physics movement based on input values
    private void FixedUpdate()
    {
        // move
        if (playerInput != Vector2.zero)
        {
            rb.AddForce(multiplyingFactor * playerInput * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
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

