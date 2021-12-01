using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* - Crear un component que dona moviment autònom al personatge.
 * Camina la distància en unitats , dona mitja volta i tornar 
 * a "patrullar" en direcció contrària. 
 * - Bonus: a l'acabar un recorregut, el personatge ha de parar,
 * inclosa animació, un temps determinat. I després continua 
 * el seu patrullatge.
 * */

public class AutomaticMovement : MonoBehaviour
{

    int Distance;
    float Speed, Weight, Height;

    private bool PatrollingActive, Moving;

    private Vector3 Scale;
    private bool FacingRight;

    // Start is called before the first frame update
    void Start()
    {
        Speed = gameObject.GetComponent<Player>().GetSpeed();
        Weight = gameObject.GetComponent<Player>().GetWeight();
        Height = gameObject.GetComponent<Player>().GetHeight();

        transform.position = new Vector3(0.00f, 0.00f, 0.00f);

        FacingRight = gameObject.GetComponent<SpriteManager>().FacingRight();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PatrollingActive)
        {
            StartPatrol();
        }
    }

    private void StartPatrol()
    {
        PatrollingActive = true;
        if (FacingRight) StartCoroutine(Patrol(true));
        else StartCoroutine(Patrol(false));
    }

    private IEnumerator Patrol(bool FacingRight)
    {
        if (FacingRight)
        {
            while (PatrollingActive)
            {
                StartCoroutine(Move(Distance));
                yield return new WaitUntil(() => !Moving);
                yield return new WaitForSeconds(2);
                gameObject.GetComponent<SpriteManager>().TurnAround();
                yield return new WaitForSeconds(2);
                StartCoroutine(Move(-Distance));
                yield return new WaitUntil(() => !Moving);
                yield return new WaitForSeconds(2);
                gameObject.GetComponent<SpriteManager>().TurnAround();
                yield return new WaitForSeconds(2);
                // Debug.Log("PATROLLED SUCCESSFULLY");
            }
        }
        else
        {
            while (PatrollingActive)
            {
                StartCoroutine(Move(-Distance));
                yield return new WaitUntil(() => !Moving);
                yield return new WaitForSeconds(2);
                gameObject.GetComponent<SpriteManager>().TurnAround();
                yield return new WaitForSeconds(2);
                StartCoroutine(Move(Distance));
                yield return new WaitUntil(() => !Moving);
                yield return new WaitForSeconds(2);
                gameObject.GetComponent<SpriteManager>().TurnAround();
                yield return new WaitForSeconds(2);
                // Debug.Log("PATROLLED SUCCESSFULLY");
            }
        }

    }

    private IEnumerator Move(float toMove)
    {
        while (!(toMove < 0.1 && toMove > -0.1))
        {
            Moving = true;
            yield return new WaitForSeconds(Time.fixedDeltaTime);

            if (toMove > 0.1)
            {
                transform.position += new Vector3(Height * Speed / Weight, 0.00f, 0.00f);
                toMove -= Height * Speed / Weight;

            }
            else
            {
                transform.position -= new Vector3(Height * Speed / Weight, 0.00f, 0.00f);
                toMove += Height * Speed / Weight;
            }
        }
        Moving = false;
        // Debug.Log("MOVED");
    }

    public bool GetMoving()
    {
        return Moving;
    }

    private void OnDisable()
    {
        PatrollingActive = false;
        StopAllCoroutines();   
    }

    private void OnEnable()
    {
        Start();
    }

}
