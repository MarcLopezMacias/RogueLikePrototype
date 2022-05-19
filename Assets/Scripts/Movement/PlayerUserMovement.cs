using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *   - Bonus: Crear un component que substitueix el 
 *   de moviment per un altre que dona el moviment 
 *   del personatge a l'usuari.
 *  */

public class PlayerUserMovement : MonoBehaviour
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

    private bool dashOnCooldown;
    private bool usingDash;
    [SerializeField]
    private int dashCooldown = 15;
    [SerializeField]
    private float dashDuration = 0.1f;
    [SerializeField]
    private float dashMultiplyingFactor = 9000f;

    // Start is called before the first frame update
    void Start()
    {
        Speed = gameObject.GetComponent<Player>().playerData.Speed;
        PlayerRigidBody = gameObject.GetComponent<Rigidbody2D>();

        Animator = gameObject.GetComponent<Animator>();

        dashOnCooldown = false;
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

        // Check Skill Use
        if (Input.GetButtonDown("Dash") && !dashOnCooldown)
        {
            StartCoroutine(UseDash());
        }
    }

    private void Move()
    {
        PlayerRigidBody.velocity = new Vector2(moveDirection.x * Speed, moveDirection.y * Speed);

        if (usingDash)
        {
            PlayerRigidBody.AddForce(new Vector2(moveDirection.x, moveDirection.y) * dashMultiplyingFactor, ForceMode2D.Impulse);
            gameObject.GetComponent<Player>().GodMode = true;
        }
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

    private IEnumerator UseDash()
    {
        dashOnCooldown = true;
        usingDash = true;
        yield return new WaitForSeconds(dashDuration);
        usingDash = false;
        gameObject.GetComponent<Player>().GodMode = false;
        yield return new WaitForSeconds(dashCooldown - dashDuration);
        dashOnCooldown = false;
    }
}
