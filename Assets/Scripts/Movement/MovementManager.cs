using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField]
    private bool AutomaticMovementActive, ManualMovementActive;

    private ManualMovement manual;
    private AutomaticMovement automatic;

    // Start is called before the first frame update
    void Start()
    {
        manual = gameObject.GetComponent<ManualMovement>();
        automatic = gameObject.GetComponent<AutomaticMovement>();
        automatic.enabled = false;
        manual.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("SwitchManualMovement"))
        {
            Debug.Log("SWITCHED TO MANUAL MOVEMENT");
            automatic.enabled = false;
            manual.enabled = true;
        }
        if (Input.GetButtonDown("SwitchAutomaticMovement"))
        {
            Debug.Log("SWITCHED TO AUTOMATIC MOVEMENT");
            automatic.enabled = true;
            manual.enabled = false;
        }

    }
}
