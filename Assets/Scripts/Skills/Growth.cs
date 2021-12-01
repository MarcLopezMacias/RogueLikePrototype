using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * - Habilitat gegantina: el nostre personatge tindrà 
 * l'habilitat de transformar-se en un gegant durant un 
 * temps limitat. La mida és directament proporcional a 
 * l'alçada "height" del component creat per recollir 
 * dades. Per activar l'habilitat és necessari fer clic en 
 * un botó. El personatge no creixerà de forma immediata 
 * sinó a mesura que camini fins a arribar a N vegades la 
 * seva alçada. Aquesta habilitat, un cop activada, ha de 
 * tenir un temps de refredament abans de tornar a ser 
 * activada.
 * 
 * BUG: SI EL PERSONATGE ES MOU MENTRE DURA L'HABILITAT, ES TORNA A LA MIDA ORIGINAL
 * */

public class Growth : MonoBehaviour
{

    private float MaxHeight;

    private Vector3 RegularScale;

    [SerializeField]
    private int Duration = 9, HeightMultiplier = 10;

    [SerializeField]
    private int Cooldown = 15;

    [SerializeField]
    private bool IsOnCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        RegularScale = transform.localScale;
        MaxHeight = transform.localScale.x * gameObject.GetComponent<DataPlayer>().GetHeight() * HeightMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ActivateGrowth") && !IsOnCooldown)
        {
            StartCoroutine(Grow());
        }
    }

    private IEnumerator Grow()
    {
        IsOnCooldown = true;
        for(int i = 0; i < Duration / 3; i++)
        {
            yield return new WaitForSeconds(1);
            float Height = gameObject.GetComponent<DataPlayer>().GetHeight();
            if(Height < MaxHeight)
            {
                Vector3 tempScale = transform.localScale;
                tempScale.x *= 1.1f;
                tempScale.y *= 1.1f;
                transform.localScale = tempScale;
            }
        }
        yield return new WaitForSeconds(Cooldown-Duration);
        IsOnCooldown = false;
        transform.localScale = RegularScale;
    }


}
