using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * - Crear una animació amb els sprites aportats. 
 * L'animació s'ha de fer a partir del Update (no animator). 
 * Ha de tenir una velocitat de 12 Fps.
 * */

public class SpriteManager : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    [SerializeField]
    public Sprite[] spriteArray;
    [SerializeField]
    public int currentSprite;

    private bool AnimationActive = true;
    private bool ManuallyMoving, AutomaticallyMoving;

    private float Speed;

    private Vector3 ScaleFacingRight, ScaleFacingLeft;

    IEnumerator ChangeSprite()
    {
        while(AnimationActive)
        {
            yield return new WaitForSeconds(0.08f / Speed);
            spriteRenderer.sprite = spriteArray[currentSprite];
            currentSprite++;

            if (currentSprite >= spriteArray.Length)
            {
                currentSprite = 0;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AnimationActive = false;
        Speed = gameObject.GetComponent<Player>().GetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScales();

        ManuallyMoving = gameObject.GetComponent<ManualMovement>().GetMoving();
        AutomaticallyMoving = gameObject.GetComponent<AutomaticMovement>().GetMoving();

        if (gameObject.GetComponent<ManualMovement>().enabled == true)
        {
            if (gameObject.GetComponent<ManualMovement>().FacingLeft())
            {
                FaceLeft();
            }
            else if (gameObject.GetComponent<ManualMovement>().FacingRight())
            {
                FaceRight();
            }

            if (ManuallyMoving && !AnimationActive) StartAnimation();
            else if(!ManuallyMoving && AnimationActive) StopAnimation();
        }
        else if(gameObject.GetComponent<AutomaticMovement>().enabled == true)
        {
            if (AutomaticallyMoving && !AnimationActive) StartAnimation();
            else if (!AutomaticallyMoving && AnimationActive) StopAnimation();
        }
    }

    private void UpdateScales()
    {
        Vector3 tempScale = transform.localScale;
        if(tempScale.x > 0)
        {
            ScaleFacingRight = tempScale;
            ScaleFacingLeft = ScaleFacingRight;
            ScaleFacingLeft.x = ScaleFacingRight.x * -1f;
        }
        else
        {
            ScaleFacingLeft = tempScale;
            ScaleFacingRight= ScaleFacingLeft;
            ScaleFacingRight.x = ScaleFacingLeft.x * -1f;
        }

    }

    public void StartAnimation()
    {
        currentSprite = 0;
        AnimationActive = true;
        StartCoroutine(ChangeSprite());
        // Debug.Log("ANIMATION STARTED");
    }

    public void StopAnimation()
    {
        currentSprite = 1;
        AnimationActive = false;
        StopCoroutine(ChangeSprite());
        // Debug.Log("ANIMATION STOPPED");
    }

    public void TurnAround()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        // Debug.Log("LOCAL SCALE X = " + transform.localScale.x);
    }

    public void FaceRight()
    {
        transform.localScale = ScaleFacingRight;
    }

    public void FaceLeft()
    {
        transform.localScale = ScaleFacingLeft;
    }

    public bool FacingRight()
    {
        return transform.localScale.x > 0;
    }
}
