using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IKillable, IDamageable<float>
{

    protected string Name;

    protected float Height, Speed, JumpSpeed, Weight;

    protected Vector2 InitialPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetSpeed()
    {
        return Speed;
    }

    public float GetJumpSpeed()
    {
        return JumpSpeed;
    }

    public float GetWeight()
    {
        return Weight;
    }

    public float GetHeight()
    {
        return Height;
    }

    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    public string GetName()
    {
        return Name;
    }

    public void Kill()
    {
        GameManager.Instance.PlayerDead();
    }

    public void Damage(float damageTaken)
    {
        throw new System.NotImplementedException();
    }
}
