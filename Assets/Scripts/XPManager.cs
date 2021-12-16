using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPManager : MonoBehaviour
{

    private int XP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseXP(int value)
    {
        XP += value;
    }

    public void DecreaseXP(int value)
    {
        XP -= value;
    }

    public void ResetXP()
    {
        XP = 0;
    }
}
