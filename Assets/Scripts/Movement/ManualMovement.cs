using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *   - Bonus: Crear un component que substitueix el 
 *   de moviment per un altre que dona el moviment 
 *   del personatge a l'usuari.
 *  */

public class ManualMovement : MonoBehaviour
{

    float Speed;

    float horizontalInput;
    float verticalInput;

    Vector3 ToMove;

    // Start is called before the first frame update
    void Start()
    {
        Speed = gameObject.GetComponent<Player>().GetSpeed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        ToMove = new Vector3(horizontalInput, verticalInput, 0) * Speed * Time.deltaTime;
        transform.Translate(ToMove);
        ShowMyMoves(horizontalInput, verticalInput);
    }

    public bool GetMoving()
    {
        if (MovingX() || MovingY()) return true;
        else return false;
    }

    public bool MovingX()
    {
        if (horizontalInput > 0 || horizontalInput < 0) return true;
        else return false;
    }

    public bool MovingY()
    {
        if (verticalInput > 0 || verticalInput < 0) return true;
        else return false;
    }

    public bool FacingRight()
    {
        if (horizontalInput > 0) return true;
        else return false;
    }

    public bool FacingLeft()
    {
        if (horizontalInput < 0) return true;
        else return false;
    }

    private void OnEnable()
    {
        Start();
    }

    public Vector3 GetToMove()
    {
        return ToMove;
    }

    private void ShowMyMoves(float horizontal, float vertical)
    {
        if(horizontal != 0 || vertical != 0) {
            Debug.Log("Should Move!! HORIZONTAL: " + horizontal + ". VERTICAL: " + vertical);
        }

    }
}
