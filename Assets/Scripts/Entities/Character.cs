using UnityEngine;

public class Character : MonoBehaviour
{
    public bool GodMode = false;
    protected Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
