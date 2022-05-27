using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    [SerializeField]
    public float KYSTime;

    void Start() 
    {
        StartCoroutine(KYS());
    }

    private IEnumerator KYS()
    {
        yield return new WaitForSeconds(KYSTime);
        Destroy(gameObject);
    }

}
