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
    private float Speed;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 MousePosition;

    private Vector2 moveDirection;
    private Vector2 faceDirection;

    private Rigidbody2D PlayerRigidBody;
    [SerializeField]
    private Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        Speed = gameObject.GetComponent<Player>().GetSpeed();
        PlayerRigidBody = gameObject.GetComponent<Rigidbody2D>();

        Animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInputs();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Animate();
        
        // ShowInputs();
    }

    private void ProcessInputs()
    {
        // ProcessKeyboard
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // ProcessMouse
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        faceDirection = MousePosition - gameObject.transform.position;
        faceDirection = faceDirection.normalized;
    }

    private void Move()
    {
        PlayerRigidBody.velocity = new Vector2(moveDirection.x * Speed, moveDirection.y * Speed);
    }

    private void Animate()
    {
        if(moveDirection.x != 0 || moveDirection.y != 0)
        {
            Animator.SetFloat("Horizontal", faceDirection.x);
            Animator.SetFloat("Vertical", faceDirection.y);
        } else
        {
            if (faceDirection.x > 0 && faceDirection.x > faceDirection.y) Animator.SetFloat("Horizontal", 0.1f);
            else if (faceDirection.x < 0 && faceDirection.x < faceDirection.y) Animator.SetFloat("Horizontal", -0.1f);
            else if (faceDirection.y > 0 && faceDirection.y > faceDirection.x) Animator.SetFloat("Vertical", 0.1f);
            else if (faceDirection.y < 0 && faceDirection.y < faceDirection.x) Animator.SetFloat("Vertical", -0.1f);

        }
    }

    private void OnEnable()
    {
        Start();
    }

    private void ShowInputs()
    {
        // Keyboard Inputs
        if(horizontalInput != 0 || verticalInput != 0) {
            Debug.Log("Should Move!! HORIZONTAL: " + horizontalInput + ". VERTICAL: " + verticalInput);
        }

        // Mouse inputs
        Debug.Log("Should be facing: " + faceDirection);

    }
}
