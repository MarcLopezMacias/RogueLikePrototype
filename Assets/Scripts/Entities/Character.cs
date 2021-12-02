using UnityEngine;

public class Character : MonoBehaviour, IKillable, IDamageable<float>, IHealable<float>
{

    protected string Name;

    protected float Height, Speed, JumpSpeed, Weight;

    protected Vector2 InitialPosition;

    protected float Health, Lifes;
    protected int MaxLifes;

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
        Lifes -= 1;
        if(Lifes <= 0 && !CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    public void Damage(float damageTaken)
    {
        Health -= damageTaken;
        if(Health <= 0)
        {
            Kill();
        }
    }

    public void IncreaseLifes(int amount)
    {
        if (Lifes < MaxLifes) Lifes += amount;
    }

    public void Heal(float amountHealed)
    {
        Health += amountHealed;
    }
}
